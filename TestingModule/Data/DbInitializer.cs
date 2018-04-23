using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingModule.Models;

namespace TestingModule.Data
{
    public class DbInitializer
    {
        public static void Initialize(TestsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Subjects.Any())
            {
                return;
            }

            var subjects = new Subject[]
            {
                new Subject{Name="Тематика 1"},
                new Subject{Name="Тематика 2"}
            };
            foreach (Subject s in subjects)
                context.Subjects.Add(s);
            context.SaveChanges();

            var rubrics = new Rubric[]
            {
                new Rubric{Name="Тема 11",SubjectId=1 },
                new Rubric{Name="Тема 12",SubjectId=1 },
                new Rubric{Name="Тема 21",SubjectId=2 },
                new Rubric{Name="Тема 22",SubjectId=2 }
            };
            foreach (Rubric r in rubrics)
                context.Rubrics.Add(r);
            context.SaveChanges();

            var duties = new Duty[]
            {
                new Duty{Name="Задание 111", RubricId=1},
                new Duty{Name="Задание 112", RubricId=1},
                new Duty{Name="Задание 121", RubricId=2},
                new Duty{Name="Задание 122", RubricId=2},
                new Duty{Name="Задание 211", RubricId=3},
                new Duty{Name="Задание 212", RubricId=4},
                new Duty{Name="Задание 221", RubricId=4},
                new Duty{Name="Задание 222", RubricId=4}
            };
            foreach (Duty d in duties)
                context.Duties.Add(d);
            context.SaveChanges();

            var tests = new UnitTest[]
            {
                new UnitTest{Value="Тест 1111", DutyId=1},
                new UnitTest{Value="Тест 1112", DutyId=1},
                new UnitTest{Value="Тест 1121", DutyId=1},
                new UnitTest{Value="Тест 1122", DutyId=1},
                new UnitTest{Value="Тест 1211", DutyId=1},
                new UnitTest{Value="Тест 1212", DutyId=1},
                new UnitTest{Value="Тест 1221", DutyId=1},
                new UnitTest{Value="Тест 1222", DutyId=1},
                new UnitTest{Value="Тест 2111", DutyId=1},
                new UnitTest{Value="Тест 2112", DutyId=1},
                new UnitTest{Value="Тест 2121", DutyId=1},
                new UnitTest{Value="Тест 2122", DutyId=1},
                new UnitTest{Value="Тест 2211", DutyId=1},
                new UnitTest{Value="Тест 2212", DutyId=1},
                new UnitTest{Value="Тест 2221", DutyId=1},
                new UnitTest{Value="Тест 2222", DutyId=1},
            };
            foreach (UnitTest t in tests)
                context.UnitTests.Add(t);
            context.SaveChanges();

        }
    }
}
