using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _01.Class_Student
{
    class Problems1to12
    {
        static void Main(string[] args)
        {
            //Problem 1.	Class Student
            var students = new List<Student>
		{
			new Student("Elena", "Bojilova",30,900020915,"+359 2 89430817","elena.bojilova@abv.bg",new List<int> {2, 4, 5, 6},1,"LONDON"),
			new Student("Katya", "Marincheva",30,901020915,"02 899430817","katya@abv.bg",new List<int> {5, 6, 5, 6},1, "LONDON"),
			new Student("Nikolay", "Ivanov",17,921020915,"+359 897430826","nikola@abv.bg",new List<int> {6, 2, 6, 2},2,"PARIS"),
			new Student("Viktor", "Kazakov",24,924020915,"+359297456826","viktor@gmail.com",new List<int> {2, 6, 6, 6},2, "PARIS"),
			new Student("Filip", "Kolev",21,924030914,"+359 897434561","filip.k@ght.gh",new List<int> {2, 6, 6, 6},1, "LONDON"),
			new Student("Nikolay", "Paunov",25,924050915,"+359 845634561","paunov@abv.bg",new List<int> {4, 2, 6, 6},2,"PARIS"),
			new Student("Asen", "Tahchiski",22,024056914,"+359 845632345","asen@hotmail.com",new List<int> {6, 6, 6, 6},3,"NEW YORK"),
		};

            Console.WriteLine("Problem 2.	Students by Group");
            var groupTwoStudents =//parvi nachin Linq-queries
                from st in students
                where st.GroupNumber == 2
                orderby st.FirstName
                select st;
            var group2 = students.Where(st => st.GroupNumber == 2).OrderBy(st => st.FirstName);//vtori nachin Linq-Extension Methods
            PrintCollection(group2);
            Console.WriteLine();

            Console.WriteLine("Problem 3.	Students by First and Last Name");
            var studentsFirstAndLastName =
               from st in students
               where st.FirstName.CompareTo(st.LastName) < 0
               select st;
            var studentsNameOrder = students.Where(x => x.FirstName.CompareTo(x.LastName) < 0);
            PrintCollection(studentsNameOrder);
            Console.WriteLine();

            Console.WriteLine("Problem 4.	Students by Age");
            var studentsByAge =
                from st in students
                where (st.Age >= 18 && st.Age <= 24)
                select new { st.FirstName, st.LastName, st.Age };
            var studentAgeAnnonymos =
                students.Where(st => st.Age >= 18 && st.Age <= 24).Select(st => new { st.FirstName, st.LastName, st.Age });
            PrintCollection(studentAgeAnnonymos);
            Console.WriteLine();

            Console.WriteLine("Problem 5.	Sort Students");
            var sortStudentsLINQquery =
                from st in students
                orderby st.FirstName descending, st.LastName descending
                select st;
            var studentsLinqExtMethods = students.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName);
            PrintCollection(studentsLinqExtMethods);
            Console.WriteLine();

            Console.WriteLine("Problem 6.	Filter Students by Email Domain");
            var studentsByEmails =
               from st in students
               where st.Email.Contains("@abv.bg")
               select st;
            var studentsABV = students.Where(st => st.Email.Contains("abv.bg")).Select(st => new { st.FirstName, st.LastName, st.Email });
            PrintCollection(studentsABV);
            Console.WriteLine();

            Console.WriteLine("Problem 7.	Filter Students by Phone");
            var studentsPhoneSofia =
                from st in students
                where (st.Phone.StartsWith("02") || st.Phone.StartsWith("+3592") || st.Phone.StartsWith("+359 2"))
                select st;
            var phoneSofia =
                students.Where(
                    st => st.Phone.StartsWith("02") || st.Phone.StartsWith("+3592") || st.Phone.StartsWith("+359 2"));
            PrintCollection(phoneSofia);
            Console.WriteLine();

            Console.WriteLine("Problem 8.	Excellent Students");
            var excellentStudents =
               from st in students
               where (st.Marks.Max() == 6)
               select new { Fullname = string.Join(" ", st.FirstName, st.LastName), Marks = string.Join(" ", st.Marks) };
            var studentsHas6 =
                students.Where(st => st.Marks.Max() == 6).Select(st => new { Fullname = string.Join(" ", st.FirstName, st.LastName), Marks = string.Join(" ", st.Marks) });
            PrintCollection(studentsHas6);
            Console.WriteLine();

            Console.WriteLine("Problem 9.	Weak Students");
            var weakStudents = students.Where(st => st.Marks.Count(x => x == 2) == 2);
            PrintCollection(weakStudents);
            Console.WriteLine();

            Console.WriteLine("Problem 10.	Students Enrolled in 2014");
            var enroledIn2014 =
                from st in students
                where (st.FacultyNumber % 100 == 14)
                select st;
            var joned2014 =
                students.Where(st => st.FacultyNumber % 100 == 14)
                    .Select(st => new { st.FirstName, st.LastName, st.FacultyNumber });
            PrintCollection(joned2014);
            Console.WriteLine();

            Console.WriteLine("Problem 11.	* Students by Groups");
            var studentsByGroups =
                from st in students
                group st by st.GroupName;
            var studentsGropuNames = students.Select(st => new { st.FirstName, st.LastName, st.GroupName }).GroupBy(st => st.GroupName);
            foreach (var group in studentsGropuNames)
            {
                Console.WriteLine(group.Key);
                PrintCollection(group);
            }
            Console.WriteLine();

            Console.WriteLine("Problem 12.	* Students Joined to Specialties");
            var specialitiesFacNums = new List<StudentSpecialties>
		                            {
			                            new StudentSpecialties("Web Developer",900020915),
			                            new StudentSpecialties("Web Developer",901020915),
			                            new StudentSpecialties("PHP Developer",921020915),
			                            new StudentSpecialties("PHP Developer",924020915),
			                            new StudentSpecialties("QA Engineer",924030914),
			                            new StudentSpecialties("QA Engineer",924050915),
			                            new StudentSpecialties("Web Developer", 024056914)
		                            };
            var jonedData =
                from student in students
                orderby student.FirstName ascending, student.LastName ascending
                join speciality in specialitiesFacNums on student.FacultyNumber equals speciality.FacultyNumber
                select new { student.FirstName, student.LastName, student.FacultyNumber, speciality.Speciality };
            var jonedDataLinqExtMtd =
                students.OrderBy(st => st.FirstName)
                    .ThenBy(st => st.LastName)
                    .Join(specialitiesFacNums, st => st.FacultyNumber, sp => sp.FacultyNumber,
                        (student, speciality) =>
                            new { student.FirstName, student.LastName, student.FacultyNumber, speciality.Speciality });
            PrintCollection(jonedDataLinqExtMtd);
            Console.WriteLine();
        }

        static void PrintCollection(IEnumerable collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}
