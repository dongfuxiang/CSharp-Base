#define MyDef
#define DoTrace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;

namespace performance
{
#if MyDef

#endif

    class Program
    {
        static int n;
        static int m;
        delegate int Delegate_(int a);
        delegate void Delegate_01();
        delegate void Delegate_02(int a);
        delegate int Delegate_03(int a);
        public static int MyFunc_01(int a) { return a = 1; }
        static void Main(string[] args)
        {

            MyBaseClass myClass = new MyBaseClass(1, 2);
            MyDerivedClass myDerClass = new MyDerivedClass(1, 2);
            //myClass.MyFunc01(ref n);
            //myClass.MyFunc02(out int m);
            //myClass.Add(1, 2);
            //myClass.Add(1, 2, 3);
            //myClass.Add(b: 1, a: 3);//命名参数，可以提供更多信息

            //myClass.VirFunc();
            //myDerClass.VirFunc();
            //int xx = myClass.VirProperty;
            //xx = ((MyBaseClass)myDerClass).VirProperty;

            //SeClass seClass = new SeClass();
            //seClass.SeFunc();
            //seClass.StFunc();

            #region 一些操作符


            {
                Console.WriteLine("Val1\t5 Val2\t10");
                Console.WriteLine("Add\x000ASome\u0007Interest");
                Console.WriteLine(10.0 / 3 * 4);
                Console.WriteLine((10.0 / 3) * 4);
                Console.WriteLine(10.0 / (3 * 4));

                byte a = 0;
                byte b = 200;
                int c = b & a;
                c = a | b;
                c = a ^ b;
                c = ~a;
                c = ~b;
            }

            #endregion

            {
                //通过反射给类成员赋值
                Type t = typeof(MyBaseClass);
                var p = t.GetProperties();
                foreach (var item in p)
                {
                    if (item.Name == "MyProperty")
                    {
                        item.SetValue(myClass, 200);
                    }
                }

                //using 语句释放资源
                using (MyBaseClass vvv = new MyBaseClass(1, 2))
                {

                }
            }


            #region 枚举示例
            {
                MyEnum myEnum = MyEnum.First | MyEnum.Second; //结果为3
                bool useFirst = myEnum.HasFlag(MyEnum.First);
                string s1 = myEnum.ToString(); //寻找值为3的枚举成员，若没有则输出3

                MyEnum _myEnum = MyEnum.First;
                string s2 = _myEnum.ToString();

                CardDeckSettings ops = CardDeckSettings.Animation | CardDeckSettings.FancyNumbers | CardDeckSettings.SingleDeck;
                bool useFancyNumbers = ops.HasFlag(CardDeckSettings.FancyNumbers);
                string s3 = ops.ToString(); //若有[Flags]特性，则输出"SingleDeck, FancyNumbers, Animation"字符串


                string[] s = typeof(CardDeckSettings).GetEnumNames();
                //或 string[] s = Enum.GetNames(typeof(CardDeckSettings));
                CardDeckSettings _p = (CardDeckSettings)Enum.Parse(typeof(CardDeckSettings), s[0]);
            }

            #endregion

            #region 数组
            {
                int[] arr1 = new int[1];//一维数组
                int[,] arr2 = new int[2, 2];//矩形数组，声明时指定每个维度的长度
                int[][] arr3 = new int[2][];//交错数组，声明时只指定顶层数组的长度
                var arr4 = new[] { 1, 2 };//隐式声明

                int x1 = arr1.Rank;//数组的维度
                int x2 = arr2.Rank;
                int x3 = arr3.Rank;

                int l1 = arr1.Length;//数组所有维度元素的总和
                int l2 = arr2.Length;
                l2 = arr2.GetLength(1);//指定维度的长度
                int l3 = arr3.Length;
                arr3[0] = new int[2];
                arr3[1] = new int[2];
                l3 = arr3.GetLength(0);

                int[][] c = (int[][])arr3.Clone();//复制数组
            }
            #endregion

            #region 委托
            #region 不带返回值的委托
            {
                //声明委托变量
                MyDel del;
                //创建随机数生成器，得到0到99的随机数
                Random random = new Random();
                int randomValue = random.Next(99);
                //创建一个包含PrintLow或PrintHigh的委托对象并将其赋值给del变量
                MyDelClass myDelClass = new MyDelClass();
                //创建委托并保存引用
                del = randomValue < 50 ? new MyDel(myDelClass.PrintLow) : new MyDel(myDelClass.PrintHigh);
                del = myDelClass.PrintHigh;//隐式声明创建委托

                //执行委托
                del(randomValue);
                //为委托添加/移除方法，由于委托不可变，所以添加/移除是指向了一个全新的委托
                del += myDelClass.Add;
                del -= myDelClass.Add;
            }

            #endregion


            #region 带返回值的委托
            {
                MyDelClass_02 myDelClass02 = new MyDelClass_02();
                MyDel_02 myDel = myDelClass02.Add1;
                myDel += myDelClass02.Add2;
                //只返回调用列表最后一个函数的返回值
                Console.WriteLine("{0} 带返回值委托", myDel());
            }

            #endregion

            #region 匿名方法
            {
                //格式：关键字 参数列表 语句块
                //不带参数
                Delegate_01 del_01 = delegate () { };
                //带参数
                Delegate_02 del_02 = delegate (int x) { };
                //带返回值
                Delegate_03 del_03 = delegate (int x) { return 1; };
                //可省略关键字后面的圆括号参数列表
                del_03 = delegate { return 1; };

            }
            #endregion


            #region Lambda表达式
            //Lambda表达式替代匿名方法
            //1.取消delegate关键字，在参数列表和匿名方法主体之间放Lambda运算符=>
            {
                Delegate_ del = delegate (int a) { return a + 1; };//匿名方法
                Delegate_ le1 = (int a) => { return a + 1; };//Lambda表达式
                //进一步简化
                //2.编译器可以从委托的声明中知道委托参数的类型，因此Lambda表达式可以省略类型参数(除非有ref out参数)
                //带有类型的参数列表称为显示类型，省略类型的参数列表称为隐式类型
                Delegate_ le2 = (a) => { return a + 1; };//Lambda表达式
                //3.如果只有一个隐式类型参数，可以省略圆括号
                Delegate_ le3 = a => { return a + 1; };//Lambda表达式
                //4.最后，Lamabda表达式的主体可以是语句块或表达式，如果语句块只包含一个返回语句，可以将语句块替换为return关键字后的表达式
                Delegate_ le4 = a => a + 1;//Lambda表达式
            }

            #endregion
            #endregion

            #region 事件
            {
                //3.事件的订阅，此类是订阅者
                myClass.MyTestEvent += MyClass_MyTestEvent;
                myClass.MyTestEvent += MyClass_MyTestEvent1;
                //触发事件
                myClass.DoEvent();
            }
            #endregion

            #region 接口
            {
                var myInt = new[] { 20, 4, 16, 9, 2 };
                MyClass[] mcArr = new MyClass[5];
                for (int i = 0; i < mcArr.Length; i++)
                {
                    mcArr[i] = new MyClass();
                    mcArr[i].TheValue = myInt[i];
                }
                //数组的排序，Sort使用的算法依赖于元素的CompareTo方法来决定两个元素的次序；
                Console.Write("排序后：");
                Array.Sort(mcArr);
                for (int i = 0; i < mcArr.Length; i++)
                {
                    Console.Write("{0} ", mcArr[i].TheValue);
                }

                #region 自定义接口
                MyIfcClass1 myIfcClass1 = new MyIfcClass1(5, 6);
                MyIfcClassDo myIfcClassDo = new MyIfcClassDo();
                //虽然PrintOut的参数是IMyInterface，但MyIfcClass1继承了此接口
                Console.WriteLine();
                myIfcClassDo.PrintOut(myIfcClass1);
                #endregion
            }


            #endregion

            #region 转换
            char ch = 'a';
            ushort sh = ch;
            {

                #region 溢出检测上下文
                ushort shh = 2000;
                byte sb;
                //在unchecked上下文中回忽略溢出，结果值是208
                sb = unchecked((byte)shh);
                Console.WriteLine("sb：{0}", sb);
                //在checked上下文中，会抛出OVerflowException异常
                //sb = checked((byte)shh);
                Console.WriteLine("sb：{0}", sb);
                #endregion
                #region 隐式引用转换
                {
                    //\\1.转换为object
                    object ob = myClass;
                    //\\2.任何类、类型转换到它实现的接口
                    MyIfcClass1 myIfcClass1 = new MyIfcClass1(1, 2);
                    IMyInterface myInterface = myIfcClass1;
                    int m = 1;
                    IComparable comparable = m;//int类型实现了IComparable接口
                                               //\\3.派生类转换到基类
                    MyBaseClass myBaseClass = new MyBaseClass(1, 2);
                    MyDerivedClass myDerivedClass = new MyDerivedClass(1, 2);
                    myBaseClass = myDerivedClass;
                }
                #endregion
                #region 显示引用转换
                //从一个普通类型到一个更加精确的引用转换
                //MyBaseClass转换为MyDerivedClass，MyDerivedClass会尝试访问它的成员，如果有成员不包含在MyBaseClass中，则内存中没有此成员，会报InvalidCastException异常
                //\\1.从object到任何引用类型
                object o = new object();
                //myClass = (MyBaseClass)o;//InvalidCastException异常
                //\\2.从基类到其继承类的转换
                //myDerClass = (MyDerivedClass)myClass;//InvalidCastException异常
                #endregion
                #region 有效显示引用转换
                {
                    //\\1.显示转换是没有必要的
                    //\\2.原引用是null
                    //\\3.由原引用指向的实际数据可以被安全的进行隐式转换
                    MyDerivedClass myDerived = new MyDerivedClass(1, 2);
                    //派生类隐式转换为基类，此时myBase指向的还是myDerived的数据，只是只能访问myBase中包含的成员
                    MyBaseClass myBase = myDerived;
                    //myBase.AAAA;编译错误
                    //基类显示转换为派生类，此时myDerived1指向依旧是myDerived的数据，可以访问派生类中的成员了，且不会报InvalidCastException异常
                    MyDerivedClass myDerived1 = (MyDerivedClass)myBase;
                    int a = myDerived1.AAAA;
                }
                #endregion
                #region 装箱转换
                //装箱是值类型转换为引用类型的隐式转换，创建了一个引用类型副本
                //\\1.创建并初始化值类型
                int i = 10;
                //\\2.创建并初始化引用类型，装箱
                object oi = i;
                i = 12;
                oi = 15;
                Console.WriteLine("i：{0},oi：{1}", i, oi);
                #endregion
                #region 拆箱转换
                //拆箱转换是显示转换
                int j = 10;
                object oj = j;
                j = (int)oj;
                #endregion
                #region 用户自定义转换
                Person bill = new Person("Bill", 25);
                //将Person转换为int，因为用的时implicit所以是隐式转换
                int age = bill;
                Console.WriteLine("Person Info：{0} {1}", bill.Name, age);
                //将Int转换为Person
                Person anno = 35;
                Console.WriteLine("Person Info：{0} {1}", anno.Name, anno.Age);
                #endregion
                #region is运算符
                //检查转换是否会成功完成，如果可以返回true，避免InvalidCastException异常
                //\\is运算符只可以用于引用转换、装箱及拆箱
                {
                    MyBaseClass myBaseClass = new MyBaseClass(1, 2);
                    MyDerivedClass myDerivedClass = new MyDerivedClass(1, 2);
                    bool res = myBaseClass is MyDerivedClass;
                    Console.WriteLine("MyBaseClass -> MyDerivedClass result：{0}", res);
                    res = myDerivedClass is MyBaseClass;
                    Console.WriteLine("MyDerivedClass -> MyBaseClass result：{0}", res);
                }
                #endregion
                #region as运算符
                //as运算符与强制转换类似，只是它不抛异常，如果转换失败返回null
                //\\as运算符只可以用于引用转换、装箱及拆箱
                {
                    MyBaseClass myBaseClass = new MyBaseClass(1, 2);
                    MyDerivedClass myDerivedClass = new MyDerivedClass(1, 2);
                    //myBaseClass = myDerivedClass as MyBaseClass;
                    myDerivedClass = myBaseClass as MyDerivedClass;//此处返回空
                }
                #endregion
            }
            #endregion

            #region 泛型
            {
                MyBaseClass myBase = new MyBaseClass(1, 2);
                int a = 5;
                MyTClass<MyBaseClass, int> myTClass = new MyTClass<MyBaseClass, int>(myBase, a);
                Console.WriteLine(myTClass.MyTypes());
                var intArray = new int[] { 1, 3, 5, 7 };
                var stringArray = new string[] { "first", "second", "third" };
                var doubleArray = new double[] { 3.14, 5.12, 6.32 };
                //泛型方法
                myTClass.PrintArray<int>(intArray);
                myTClass.PrintArray(intArray);//推断类型并调用
                myTClass.PrintArray(stringArray);
                myTClass.PrintArray(doubleArray);
                //泛型拓展方法
                myTClass.ExtendFunc(2);
                //泛型委托
                MyTDel<string> myTDel = new MyTDel<string>(myTClass.PrintString);
                myTDel += myTClass.PrintUpperString;
                myTDel("Hi,there!");
                //带返回值的泛型委托
                var myFunc = new MyFunc<int, int, string>(myTClass.PrintSome);
                Console.WriteLine("MyFunc<int, int, string>：" + myFunc(4, 5));

            }

            #endregion

            #region 枚举器与迭代器
            {
                #region 使用IEnumerator与IEnumerable示例
                Spectrum spectrum = new Spectrum();
                //因为spectrum包含IEnumerator GetEnumerator()可使用foreach
                Console.Write("使用IEnumerator与IEnumerable：");
                foreach (var item in spectrum)
                {
                    Console.Write(" " + item);
                }
                Console.WriteLine();
                #endregion

                #region 使用迭代器创建枚举器
                string[] Colors = new string[] { "violet", "blue", "cyan", "green", "yellow", "orange", "red" };
                ColorIterator colorIterator = new ColorIterator(Colors);
                Console.Write("迭代器创建枚举器：");
                foreach (var item in colorIterator)
                {
                    Console.Write(" " + item);
                }
                Console.WriteLine();
                #endregion

                #region 使用迭代器创建可枚举类型
                ColorIterable colorIterable = new ColorIterable(Colors);
                Console.Write("迭代器创建可枚举类型：");
                //使用类对象
                foreach (var item in colorIterable)
                {
                    Console.Write(" " + item);
                }
                Console.WriteLine();
                //使用类枚举方法，这样即使不实现GetEnumerator()方法，也可以使用迭代器返回的可枚举类，只需要直接使用迭代器方法；
                foreach (var item in colorIterable.BlackAndWhite())
                {
                    Console.Write(" " + item);
                }
                Console.WriteLine();
                #endregion

                #region 迭代器作为属性
                ColorProp propUV = new ColorProp(true, Colors);
                ColorProp propIR = new ColorProp(false, Colors);
                Console.Write("迭代器作为属性：");
                foreach (var item in propUV)
                {
                    Console.Write(" " + item);
                }
                Console.WriteLine();
                foreach (var item in propIR)
                {
                    Console.Write(" " + item);
                }
                Console.WriteLine();
                #endregion
            }


            #endregion

            #region LINQ

            #region 匿名类型
            ClassTset1 classTset1 = new ClassTset1();
            classTset1.Fc();
            #endregion

            #region 方法语法和查询语法
            //\\1.方法语法使用标准的方法调用；
            //\\2.查询语法看上去和SQL语句很相似，使用查询表达式书写；
            {
                var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 10 };
                //查询语法
                //返回一个可枚举类型
                IEnumerable<int> numberQuery = from n in numbers
                                               where n < 5
                                               select n;
                //方法语法
                //返回一个可枚举类型
                IEnumerable<int> numberMethod = numbers.Where(delegate (int x) { return x < 5; });
                numberMethod = numbers.Where(x => x < 5);
                //两种形式组合使用
                int numsCount = (from n in numbers
                                 where n < 5
                                 select n).Count();

                Console.WriteLine("查询语法：");
                foreach (var item in numberQuery)
                {
                    Console.Write("{0}", item);
                }
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("方法：");
                foreach (var item in numberMethod)
                {
                    Console.Write("{0}", item);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            #endregion

            #region 查询表达式的结构

            #region 1.from字句
            //\\1.from子句指定了要作为数据源使用的数据集合；
            //\\2.from创建可执行查询的后台代码对象，只有在程序的控制流遇到访问查询变量语句时，才会执行查询；
            //\\3.结构：from Type Item in Items
            //\\        Type是集合中元素类型，可选，编译器会自动推断；
            //\\        Item是迭代变量的名字，迭代变量逐个表示数据源的每一个元素，并会被之后的Where和Select选择或丢弃；
            //\\        Items是要查询的集合的名字，集合必须是可枚举的；
            {
                int[] arr = new int[] { 1, 5, 7, 10, 15 };
                var query = from item in arr
                            where item < 10 //where丢弃集合中大于10的元素
                            select item;
                Console.Write("from：");
                foreach (var item in query)
                {
                    Console.Write("{0} ", item);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            #endregion

            #region 2.join子句
            //\\1.使用联结结合多个集合
            //\\2.联结操作接受两个集合，然后创建一个临时的对象集合
            //\\3.结构：join Identifier in Collection2 on Field1 equals Field2 
            //\\        指定了第二个集合和之前的集合进行联结
            {
                var students = new[]
            {
                new { StID = 1, LastName = "Carson" },
                new { StID = 2, LastName = "Klassen" },
                new { StID = 3, LastName = "Fleming" }
            };

                var studentsInCoures = new[]
                {
                    new { CouresName="Art",StID=1 },
                    new { CouresName="Art",StID=2 },
                    new { CouresName="History",StID=1 },
                    new { CouresName="History",StID=3 },
                    new { CouresName="Physics",StID=3 },
                };

                //查询出选了历史课的学生得到信息
                var query = from s in students
                            join c in studentsInCoures on s.StID equals c.StID
                            where c.CouresName == "History"
                            select c.CouresName;

                Console.Write("join：");
                foreach (var item in query)
                {
                    Console.Write("{0} ", item);
                }
                Console.WriteLine();
                Console.WriteLine();
            }



            #endregion

            #region 3.from...let...where
            //\\1.let子句接受一个表达式的运算并且把它赋值给一个需要在其他运算中使用的标识符；
            //\\2.结构：let Identifier = Expression；
            //\\3.where子句根据之后的运算来去除不符合指定条件的项；
            //\\4.结构：where BooleanExpression
            {
                var groupA = new[] { 3, 4, 5, 6 };
                var groupB = new[] { 6, 7, 8, 9 };
                var someInts = from a in groupA
                               from b in groupB
                               let sum = a + b
                               where sum > 11 //条件1
                               where a == 4   //条件2
                               select new { a, b, sum }; //返回枚举
                Console.Write("from...let...where：");
                foreach (var item in someInts)
                {
                    Console.Write("{0} ", item);
                }
                Console.WriteLine();
                Console.WriteLine();

            }

            #endregion

            #region 4.orderby子句
            //\\1.orderby子句接受一个表达式并根据表达式顺序返回结果项
            //\\2.结构：orderby Expression(ascending/descending)

            {
                var students = new[]
                {
                    new { LName="Jones",FName="Mary",Age=19,Major="History"},
                    new { LName="Simth",FName="Bob",Age=20,Major="CompSci"},
                    new { LName="Fleming",FName="Carol",Age=21,Major="History"},
                };
                var query = from s in students
                            orderby s.Age descending //根据年龄倒叙
                            select s;
                Console.Write("orderby：");
                foreach (var item in query)
                {
                    Console.Write("{0} ", item);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            #endregion

            #region 5.select...group子句
            //select...group部分之前指定数据源和要选择的对象
            //\\1.select子句指定所选对象的哪部分应该被select，他可以是整个数据项、数据项的一个字段、数据项中几个字段组成的新对象（匿名类）
            //\\2.group...by子句是可选的，用来指定选择的项如何被分组
            //\\3.group子句把select的对象根据标准进行分组，group返回项的分组的枚举类型,最为分组依据的属性叫做键
            //\\4.结构：select Expression (group Expression1 by Expression2)
            {
                var students = new[]
               {
                    new { LName="Jones",FName="Mary",Age=19,Major="History"},
                    new { LName="Simth",FName="Bob",Age=20,Major="CompSci"},
                    new { LName="Fleming",FName="Carol",Age=21,Major="History"},
                };
                var query = from student in students
                            group student by student.Major;

                Console.Write("group...by：");
                foreach (var item in query)
                {
                    Console.Write("    {0}分组： ", item.Key);
                    foreach (var item_ in item)
                    {
                        Console.Write("{0}", item_);
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            #endregion

            #region 6.查询延续 into子句
            //\\1.查询延续子句可以接受查询的一部分结果并赋予一个名字，从而可以在查询的另一部分使用
            //\\2.结构 into Type Identifier inExpression on Experssion equals Expression 
            {
                var groupA = new[] { 3, 4, 5, 6, 7 };
                var groupB = new[] { 4, 5, 6, 7 };
                var someInts = from a in groupA
                               join b in groupB on a equals b
                               into groupAandB
                               from c in groupAandB
                               select c;
                Console.Write("into：");
                foreach (var item in someInts)
                {
                    Console.Write("{0} ", item);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            #endregion


            #endregion

            #region 标准查询运算符
            //\\1.被查询的集合对象必须是序列，也就是实现了IEnumerable<T>接口，因为标准查询运算符都是IEnumerable<T>的拓展方法
            //\\2.使用方法语法
            //\\3.一些运算符返回可枚举类型，一些返回一个标量
            //\\4.多数操作符以谓词作为参数。谓词是一个方法，以集合中的对象为参数，根据其是否满足某个条件返回true或false。
            //\\    例如 public static int Count<T>(this IEnumerable<T>,Func<T,bool> predicate)
            {
                var intArray = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                Func<int, bool> func = new Func<int, bool>(x => x % 2 == 1);//匿名函数显示创建委托
                func = x => x % 2 == 1;//匿名函数隐式创建委托，与上一个创建委托方式等价
                //当形参是委托时，可以传入同返回值与参数列表的函数
                Test(func);
                Test(x => x % 2 == 1);


                //找到数组中的奇数，以下三种方式等价
                //1.将委托作为参数
                var countOdd = intArray.Count(func);
                //2.匿名函数作为参数
                countOdd = intArray.Count(delegate (int x) { return x % 2 == 1; });
                //3.将Lambda表达式作为参数
                countOdd = intArray.Count(x => x % 2 == 1);

            }
            #endregion

            #region LINQ TO XML
            #region XML
            {
                XDocument Students =
                    new XDocument(            //创建XML文档
                        new XElement("Students",   //创建根节点

                    new XElement("Student",//创建第一个Student元素
                    new XAttribute("Height", "99"), //创建特性
                    new XAttribute("Width", "99"),  //创建特性
                    new XElement("Name", "张三"),//创建元素
                    new XElement("PhoneNumber", "111111")//创建元素
                    ),

                    new XElement("Student",//创建第二个Student元素
                    new XElement("Name", "王五"),//创建元素
                    new XElement("PhoneNumber", "222222")//创建元素
                    ),
                    new XElement("Student",//创建第二个Student元素
                    new XElement("Name", "赵六"),//创建元素
                    new XElement("PhoneNumber", "222222"),//创建元素
                    new XElement("PhoneNumber", "444444")//创建元素
                    )

                    )
                    );
                Students.Save("Students.xml");
                //将保存的文件加载到新变量中
                XDocument xDocument = XDocument.Load("Students.xml");
                //添加元素
                XElement rt = xDocument.Root;
                rt.Add(
                    new XElement("Add_01"),
                    new XElement("Add_02"),
                    new XComment("这是注释"),
                    new XElement("Add_03")
                    );


                XElement root = xDocument.Element("Students");//获取第一个名为“Students”的子XElement，因为是根节点，所以肯定有且只有一个
                IEnumerable<XElement> elements = root.Elements();//获取根节点下所有的子XElement的可枚举类型
                foreach (XElement student in elements)
                {
                    student.SetAttributeValue("Height", "100");//更改或添加特性
                    XAttribute height = student.Attribute("Height");//获取特性
                    string value = height?.Value;
                    XElement nameNode = student.Element("Name");//获取第一个名为“Name”的子XElement
                    IEnumerable<XElement> phoneNumbers = student.Elements("PhoneNumber");//获取所有名为“PhoneNumber”的元素
                }
                xDocument.Save("Students.xml");

                Console.WriteLine("XML：");
                Console.WriteLine(xDocument);
                Console.WriteLine();


                IEnumerable<XElement> students = root.Elements("Student");//获取根节点下名字为Student的节点
                var x = from elemt in elements//或students
                        where elemt.Element("PhoneNumber")?.Value == "222222"
                        select elemt;

                Console.WriteLine("LINQ TO XML：");
                foreach (XElement y in x)
                {
                    Console.WriteLine(y.ToString());
                }
                Console.WriteLine();

            }
            #endregion

            #endregion
            #endregion

            #region 异常处理
            {
                try
                {
                    Exception e = new Exception("自定义异常！");
                    throw e;//抛出异常
                }
                catch (Exception e)
                {
                    string str = e.Message;

                }
            }
            #endregion

            #region 反射和特性
            //\\1.有关程序及其类型的数据称为元数据，对象浏览器是显示元数据的一个示例；
            //\\2.一个运行的程序查看本身或其他程序的元数据叫反射；
            #region 获取Type对象
            BaseClass bc = new BaseClass();
            DerievdClass dc = new DerievdClass();
            BaseClass[] bcs = new BaseClass[] { bc, dc };
            foreach (var val in bcs)
            {
                Type type = val.GetType();//通过实例获取Type对象
                Type t = typeof(DerievdClass);//通过对象类型获取Type对象
                Console.WriteLine();
                Console.WriteLine("Class Name：{0}", type.Name);
                Console.WriteLine("Class NameSpace：{0}", type.Namespace);
                foreach (var field in type.GetFields())
                {
                    Console.WriteLine("Class Field：{0}", field);
                }
            }


            #endregion

            #region 应用特性
            //1.Obsolete特性
            ObClass obClass = new ObClass();
            int DoTrace;
            //2.Conditional特性
            CoClass coClass = new CoClass();
            //DoTrace被定义，CoFunc被调用了
            coClass.CoFunc();
            //3.调用者信息特性
            CallClass callClass = new CallClass();
            callClass.MyTrace("调用者信息特性");
            #endregion

            #region 多个特性
            //多层结构
            // [Obsolete]
            // [Serializable]

            //逗号分割
            // [Obsolete, Serializable]

            //两种方式等价
            #endregion
            #endregion

            #region 字符串string、stringbuilder
            {
                //字符串string是不变的，任何对字符串的改变都是返回一个新的副本
                //StringBuilder可以动态有效的生产字符串，避免创建许多副本
                StringBuilder sb = new StringBuilder();
                sb.Append("StringBuilder");

                #region 把字符串解析为数字值
                //每个预定义的简单类型都有一个Parse方法
                string s = "25.376";
                double d = double.Parse(s);
                //Parse如果不能把字符串转目标类型的话会抛出异常，为了避免异常，有TryParse
                string ss = "123";
                int mm;
                bool issuccess = int.TryParse(ss, out mm);

                #endregion

            }
            #endregion

            
            foreach (var item in args)
            {
            }
        }


        private static void MyClass_MyTestEvent1(object sender, TestEventArgs e)
        {
            Console.WriteLine("事件处理器2：{0}", e.MyProperty);
        }

        //4.事件处理器，事件触发时调用
        private static void MyClass_MyTestEvent(object sender, TestEventArgs e)
        {
            Console.WriteLine("事件处理器1：{0}", e.MyProperty);
        }

        private static void Test(Func<int, bool> func)
        {

        }
    }


}
