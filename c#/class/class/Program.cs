﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace @class
{
    //一樣放在最上面
    class People{

        public int _age { get; set; }
        public string _name { get; set; }

        public People(){    //以下2個函式都是people，但是裡面參數不同（同vb一樣可以這樣做）
            _name = "王小明";
            _age = 10;
        }

        public People(int age, string name){
            _age = age;
            _name = name;
        }

        public void Eat(string food){   //這邊會要求一定要有回傳值，選擇void就是沒回傳值
            Console.WriteLine("{0} eat {1}", _name, food);
        }
    }

    class Student : People{  //繼承（學生也是人，所以學生會繼承人會做的事）
                            
        public int ID { get; set; }  //Properties的做法（而Property就是會可以有get() 和 set()函數去設定和取值）

        public void Study(string subject)
        {
            Console.WriteLine("學號{0} {1}同學在讀{2}科目", ID, this._name, subject);  //繼承後可以拿到自己的名字
        }

        public Student(int age, string name){
            this._age = age;
            this._name = name;
        }

        public Student() { }
    }

    class Teacher : People{
        public string main_subject { get; set; }  //Properties的做法（而Property就是會可以有get() 和 set()函數去設定和取值）

        public void Teach(Student student)
        {
            Console.WriteLine("{0}老師 {1}正在教{2}同學", main_subject, this._name, student._name);
        }

        public Teacher(int age, string name)
        {
            this._age = age;
            this._name = name;
        }
    }

    class Animal
    {
        private double height;

        public double Height
        {
            get { return height; }
            set { height = value + 50; }   //設定時身高都上升50
        }
    }

    //public override void Eat() 可以寫在 Student 和  Teacher中，此為覆寫，就是改掉寫成自己的function
    //原理都懂就不實作了


    class Program
    {
        static void Main(string[] args){

            People p1 = new People();
            People p2 = new People(30, "王大明");

            Console.WriteLine("{0} {1}", p1._age, p1._name);
            Console.WriteLine("{0} {1}", p2._age, p2._name);

            p1._age = 15;
            p1._name = "陳小胖";
            Console.WriteLine("{0} {1}", p1._age, p1._name);

            p1.Eat("noodles");
            p2.Eat("steak");

            Console.WriteLine("_____________________________________________________________________________");

            Student s1 = new Student();
            s1._name = "Marry";
            s1._age = 5;
            s1.ID = 2;

            Student s2 = new Student(10, "jack");
            s2.ID = 10;

            Console.WriteLine("{0} {1} {2}", s1._age, s1._name, s1.ID);
            Console.WriteLine("{0} {1} {2}", s2._age, s2._name, s2.ID);

            s1.Study("math");
            s2.Study("chinese");

            s1.Eat("rice");    //因為有繼承，所以可以用他的function
            s2.Eat("fried chicken");

            Console.WriteLine("____________________________________________________________________________-");

            Teacher t1 = new Teacher(35, "Wu");
            Teacher t2 = new Teacher(65, "Chen");

            t1.main_subject = "history";
            t2.main_subject = "english";

            Console.WriteLine("{0} {1} {2}", t1._age, t1._name, t1.main_subject);
            Console.WriteLine("{0} {1} {2}", t2._age, t2._name, t2.main_subject);

            t1.Eat("烤肉");
            t2.Eat("果汁");

            t1.Teach(s1);
            t2.Teach(s2);

            Console.WriteLine("_____________________________________________________________________");

            Animal a1 = new Animal();
            a1.Height = 50;
            Console.WriteLine(a1.Height);

            Console.Read();

        }
    }
}