using System;
using System.ComponentModel;
using System.Reflection;
using Utils;

namespace UtilityTest.Test
{
    public class EnumTest : IExecute
    {
        public void Excute()
        {
            var result = EnumUtil.GetDesciption(Category.Chinese);
            Console.WriteLine(Category.Chinese + "," + result + "," + (int)Category.Chinese);

            var myFieldObjectB = new MyFieldClassB();
            var myTypeB = typeof(MyFieldClassB);
            var myFieldInfo1 = myTypeB.GetField("field", BindingFlags.NonPublic | BindingFlags.Instance);
            if (myFieldInfo1 != null)
            {
                var attributes = myFieldInfo1.GetCustomAttributes(typeof(MyFieldClassB), false);
            }
            Console.WriteLine("The value of the private field is: '{0}'", myFieldInfo1.GetValue(myFieldObjectB));
        }
    }

    public enum Category
    {
        /// <summary>
        ///     英语
        /// </summary>
        [Description("西洋文")]
        English = 1,

        /// <summary>
        ///     汉语
        /// </summary>
        [Description("汉语")]
        Chinese = 2,

        /// <summary>
        ///     日语
        /// </summary>
        [Description("日本话")]
        Japanese = 3
    }

    public class MyFieldClassB
    {
        private string field = "B Field";

        public string Field
        {
            get { return field; }
            set
            {
                if (field != value)
                {
                    field = value;
                }
            }
        }
    }
}