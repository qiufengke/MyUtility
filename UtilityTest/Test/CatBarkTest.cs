using System;
using System.Collections;

namespace UtilityTest.Test
{
    /// <summary>
    /// 猫叫了
    /// </summary>
    public class CatBarkTest
    {
        public void Excute01()
        {
            #region 猫叫了1

            ISubject cat = new Cat();
            var m = new Mouse(cat);
            var p = new People(cat);
            cat.Cry();

            #endregion
        }

        public void Excute02()
        {
            #region 猫叫了2

            //var c = new Cat();
            //var m = new Mouse(c);
            //var p = new People(c);
            //c.OnCry();

            #endregion
        }
    }

    #region 猫叫了1

    public interface IObserver
    {
        void Consoles();
    }

    public interface ISubject
    {
        void AddObserver(IObserver obj);
        void Cry();
    }

    public partial class Cat : ISubject
    {
        private readonly ArrayList list = new ArrayList();

        public void AddObserver(IObserver obj)
        {
            list.Add(obj);
        }

        public void Cry()
        {
            Console.WriteLine("猫叫了");
            foreach (IObserver o in list)
            {
                o.Consoles();
            }
        }
    }

    public partial class People : IObserver
    {
        public People(ISubject s)
        {
            s.AddObserver(this);
        }

        public void Consoles()
        {
            Console.WriteLine("主人醒了");
        }
    }

    public partial class Mouse : IObserver
    {
        public Mouse(ISubject s)
        {
            s.AddObserver(this);
        }

        public void Consoles()
        {
            Console.WriteLine("老鼠跑了");
        }
    }

    #endregion

    #region 猫叫了2

    //public delegate void SubEventHandler();

    //public abstract class Subject
    //{
    //    public event SubEventHandler SubEvent;

    //    public void action()
    //    {
    //        if (SubEvent != null)
    //            SubEvent();
    //    }
    //}

    //public partial class Cat : Subject
    //{
    //    public void OnCry()
    //    {
    //        Console.WriteLine("猫叫了");
    //        action();
    //    }
    //}

    //public abstract class Observer
    //{
    //    protected Observer(Subject s)
    //    {
    //        s.SubEvent += Response;
    //    }

    //    public abstract void Response();
    //}

    //public partial class Mouse : Observer
    //{
    //    public Mouse(Subject s)
    //        : base(s)
    //    {
    //    }

    //    public override void Response()
    //    {
    //        Console.WriteLine("老鼠跑了");
    //    }
    //}

    //public partial class People : Observer
    //{
    //    public People(Subject s)
    //        : base(s)
    //    {
    //    }

    //    public override void Response()
    //    {
    //        Console.WriteLine("主人醒了");
    //    }
    //}

    #endregion
}