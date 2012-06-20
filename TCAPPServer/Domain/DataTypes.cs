using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
 * 
 *  TODO:
 *  - pick dates to print (saving last week off, )->read line items to db
 *  - Print ALL Employees to formatted page (tab delimited, with page breaks)
 *  - Manage Employees in database/datastore (use a obj mapper)
 *  - correct line items in page (edit/add)
 *  - assign project tasks to work
 *  - design to make web enabled ( client/server)
 *  - android app for assigning task id/labels
 *  - print checks
 */

namespace TCAPPServer.Domain
{
    enum Mode
    {
        IN = 30,
        OUT = 31,
        LUNCH_OUT = 35,
        LUNCH_IN = 34
    }

    #region Business

    public class Business
    {
        public virtual int Id { get; set; }

        public virtual List<Person> Employees{get;set;}
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }

        //TODO: Store these in a file
        //public static int[] PayRate = { 0, 0, 11, 23, 10 };
        public static string[] EmployeeNames = { "sepehr", "dad", "Juan", "Clecio", "Eduardo" };

    }
    #endregion

    #region Person Class

    [Serializable]
    public class Person
    {
        string header = "{0}" + Environment.NewLine + "Date\tDay\tIn\tLunch\tOut\tTotal Hrs" + Environment.NewLine;
        string formattedEntry = "{0}\t{1}\t{2}\t{3}\t{4}\t{5}" + Environment.NewLine;

        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
        
        private List<TimeEntry> _timeEntries;

        public virtual PayStub GetWork(DateTime startTime, DateTime endTime)
        {
            string result = string.Format(header, Business.EmployeeNames[Id-1]);
            DateTime currentDate = startTime;
            TimeSpan totalWork = new TimeSpan(0);

            while (currentDate.CompareTo(endTime) < 0)
            {
                List<TimeEntry> clockIn = _timeEntries.FindAll(d =>
                    (d.DateTime.DayOfYear == currentDate.DayOfYear &&
                        d.DateTime.Year == currentDate.Year &&
                        d.Mode == (int)Mode.IN ) );
                
                List<TimeEntry> clockOut = _timeEntries.FindAll(d =>
                    (d.DateTime.DayOfYear == currentDate.DayOfYear &&
                        d.DateTime.Year == currentDate.Year &&
                        d.Mode == (int)Mode.OUT));

                List<TimeEntry> lunchOut = _timeEntries.FindAll(d =>
                    (d.DateTime.DayOfYear == currentDate.DayOfYear &&
                        d.DateTime.Year == currentDate.Year &&
                        d.Mode == (int)Mode.LUNCH_OUT));

                List<TimeEntry> lunchIn = _timeEntries.FindAll(d =>
                    (d.DateTime.DayOfYear == currentDate.DayOfYear &&
                        d.DateTime.Year == currentDate.Year &&
                        d.Mode == (int)Mode.LUNCH_IN));


                TimeSpan lunchTime = calcLunch(lunchOut, lunchIn);
                TimeSpan workTime = calcWorkTime( clockIn, clockOut, lunchTime);
                totalWork = totalWork.Add(workTime);

                result += string.Format(formattedEntry,
                                currentDate.ToString("M/d/yy"),
                                currentDate.DayOfWeek.ToString(),
                                printTime(clockIn, Mode.IN),
                                Math.Round(lunchTime.TotalMinutes,2),
                                printTime( clockOut, Mode.OUT),
                                Math.Round(workTime.TotalHours,2) );


                currentDate = currentDate.AddDays(1);
            }

            PayStub ps = new PayStub();
            ps.Summary = result;
            ps.TotalHours = totalWork.TotalHours;
            
            return ps;
        }

        private string printTime(List<TimeEntry> entries, Mode mode)
        {
            if (entries == null)
                return "[NULL Entries: " + mode.ToString() + "]";
            if (entries.Count == 1)
                return entries[0].DateTime.ToString("T");
            if (entries.Count < 1)
                return "[NO ENTRY: " + mode.ToString() + "]";
            if (entries.Count > 1)
            {
                string entryId = string.Empty;
                foreach (TimeEntry entry in entries)
                    entryId += entry.Number + ",";
                return "[" + entries.Count + " ENTRIES:" + entryId +  mode.ToString() + "]";
            }

            return "[ERROR " + mode.ToString() + "]";
        }

        private TimeSpan calcWorkTime(List<TimeEntry> clockIn, List<TimeEntry> clockOut, TimeSpan lunchTime)
        {
            if (clockIn.Count != 1 || clockOut.Count != 1)
                return new TimeSpan(0);
            TimeSpan workTime = clockOut[0].DateTime.Subtract(clockIn[0].DateTime);
            if (lunchTime.TotalMinutes > 30)
            {
                workTime.Subtract( lunchTime );
            }
        
            return workTime;
        }

        private TimeSpan calcLunch(List<TimeEntry> lunchOut, List<TimeEntry> lunchIn)
        {
            if (lunchOut.Count != 1)
                return new TimeSpan(0,1,0);
            if (lunchIn.Count != 1)
                return new TimeSpan(0,2,0);

            TimeSpan span = lunchIn[0].DateTime.Subtract(lunchOut[0].DateTime);
            return span;
        }

        public Person()
        {
            _timeEntries = new List<TimeEntry>();
        }

        public virtual void AddTimeEntry( TimeEntry entry)
        {
            _timeEntries.Add(entry);
        }

    }

    #endregion 

    #region PayStub

    [Serializable]
    public class PayStub
    {
        public string Summary { get; set; }
        public double TotalHours { get; set; }
        public double GetPay(double rate)
        {
            double money = TotalHours * rate;
            return Math.Round(money, 2);
        }
    }

    #endregion

    #region TimeEntry
    [Serializable]
    public class TimeEntry
    {
        private string _rawEntryString;
        private int _number;
        public int Number
        {
            get { return _number; }
        }

        private int _machineId;
        public int MachineId
        {
            get { return _machineId; }
        }

        private int _employeeNumber;
        public int EmployeeNumber
        {
            get { return _employeeNumber; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        private int _managerId;
        public int ManagerId
        {
            get { return _managerId; }
        }

        private int _mode;
        public int Mode
        {
            get { return _mode; }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
        }

        public string RawEntryString { get { return _rawEntryString; } }

        public TimeEntry(string rawEntryString)
        {
            _rawEntryString = rawEntryString;
            string[] colums = _rawEntryString.Split('\t');
            _number = int.Parse(colums[0]);
            _machineId = int.Parse(colums[1]);
            _employeeNumber = int.Parse(colums[2]);
            _name = colums[3];
            _managerId = int.Parse(colums[4]);
            _mode = int.Parse(colums[5]);
            _dateTime = DateTime.Parse(colums[6]);
        }
        
    }
    #endregion

}
