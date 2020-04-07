using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bar;
using Bar.Controllers;




namespace Bar.Controllers
{
    
   

    public class HomeController : Controller
    {
        Database24Entities dc = new Database24Entities();

        public ActionResult Index()
        {

            Session.Abandon();

            return View();
        }

        public ActionResult LoginB()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginB(Login u)
        { // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                {
                    var v = dc.Logins.Where(a => a.BartenderID.Equals(u.BartenderID)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["BID"] = v.BartenderID.ToString();
                       
                        return RedirectToAction("AfterLogin");
                    }
                }
            }
            ViewBag.Message = "Your Bartender ID could not be found";
            return View(u);
        }
        public ActionResult AfterLogin()
        {
            if (Session["BID"] != null)
            {
                List<object> myModel = new List<object>();
                var b = dc.Customers.Where(x => x.Status != "Done").OrderBy(x => x.OrderID).Take(5).ToList();

                var a = dc.Customers.Where(x => x.Status != "Done").OrderBy(x => x.OrderID).Take(1).ToList();

               

                myModel.Add(a.ToList());
                myModel.Add(b.ToList());
                
                return View(myModel);

               
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult Customer()
        {
            int print;
            int maxorder = dc.Customers.OrderBy(p => p.OrderID).Max(p => p.OrderID);
            print = maxorder + 1;
            var value = print;

            int print1;
             int maxorder1 = dc.Orders.OrderBy(p=>p.PK).Max(p => p.PK);
            print1 = maxorder1+ 1;
            var value1 = print1;

            ViewBag.PKvalue = value1;
            ViewBag.Msg = value;
            ViewBag.Status = "Pending";


            ViewBag.Drink1 = "Baileys";
            ViewBag.Drink2 = "Blue";
            ViewBag.Drink3 = "Margarita";
            ViewBag.Drink4 = "LongIsland";
            ViewBag.Drink5 = "Martini";
            ViewBag.Drink6= "PinaC";

            return View();
        }




        public ActionResult Details(int OrderId)
        {
           
            if (Session["BID"] != null)
            {
                List<Object> myModel = new List<Object>();

                var customer = from r in dc.Customers
                               where r.OrderID == OrderId
                               select r;

          
                var order = from d in dc.Orders
                            where d.OrderID == OrderId
                            select d;


                myModel.Add(customer.ToList());

                myModel.Add(order.ToList());
                

                return View(myModel.ToList());

                //rules
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        // GET: Volunteer/Edit/5
        //public ActionResult Details1(string OrderId)
        //{
        //    if (OrderId == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        // Customer customer = dc.Customers.Find(OrderId);
        
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
     
        //    return View(customer);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details1([Bind(Prefix="item", Include = "OrderID,OrderName,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Status = "Done";
                dc.Configuration.ValidateOnSaveEnabled = false;
                dc.Entry(customer).State = EntityState.Modified;
                dc.SaveChanges();

                return RedirectToAction("AfterLogin");
            }
            return View("Details",customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Creation1(string Search, string Search1, string Search2, string Search3, string Search4, string Search5,
            [Bind(Include = "OrderID,Name,Quantity,PK")] Order orderb,
         [Bind(Include = "OrderID,Name,Quantity,PK")] Order orderbl,
         [Bind(Include = "OrderID,Name,Quantity,PK")] Order orderl,
         [Bind(Include = "OrderID,Name,Quantity,PK")] Order orderm,
         [Bind(Include = "OrderID,Name,Quantity,PK")] Order ordermg,
         [Bind(Include = "OrderID,Name,Quantity,PK")] Order orderp,
            [Bind(Prefix = "Customer", Include = "OrderID,OrderName,Status")] Customer customer)

          

        {
            if (Search == null || Search == "" || Search == "0")
            { Search = "0"; }
            if (Search1 == null || Search1 == "" || Search1 == "0")
            { Search1 = "0"; }
            if (Search2 == null || Search2 == "" || Search2 == "0")
            { Search2 = "0"; }
            if (Search3 == null || Search3 == "" || Search3 == "0")
            { Search3 = "0"; }
            if (Search4 == null || Search4 == "" || Search4 == "0")
            { Search4 = "0"; }
            if (Search5 == null || Search5 == "" || Search5 == "0")
            { Search5 = "0"; }
            if (int.TryParse(Search, out int result) && int.TryParse(Search1, out int result1) && int.TryParse(Search2, out int result2)
                && int.TryParse(Search3, out int resul3t) && int.TryParse(Search4, out int result4) && int.TryParse(Search5, out int result5))
            {

                if (ModelState.IsValid)
                {
                    int print1 = dc.Orders.OrderBy(p => p.PK).Max(p => p.PK); ;
                    int print;
                    int maxorder = dc.Customers.OrderBy(p => p.OrderID).Max(p => p.OrderID);

                    print = maxorder + 1;
                    int value = print;




                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.Customers.Add(customer);


                    if (Search == "0" || Search == null || Search == "")
                    { }
                    else
                    {



                        print1 = print1 + 1;


                        orderb.Quantity = Search;
                        orderb.OrderID = value;
                        orderb.Name = "Baileys";
                        orderb.PK = print1;
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.Orders.Add(orderb);


                    }
                    if (Search1 == "0" || Search1 == null || Search1 == "")
                    { }
                    else
                    {
                        print1 = print1 + 1;


                        int value1 = print1;
                        orderbl.Quantity = Search1;
                        orderbl.OrderID = value;
                        orderbl.Name = "Blue";

                        orderbl.PK = print1;

                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.Orders.Add(orderbl);


                    }
                    if (Search2 == "0" || Search2 == null || Search2 == "")
                    { }
                    else
                    {

                        print1 = print1 + 1;

                        int value1 = print1;
                        orderl.Quantity = Search2;
                        orderl.OrderID = value;
                        orderl.Name = "Margarita";
                        orderl.PK = print1;

                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.Orders.Add(orderl);


                    }
                    if (Search3 == "0" || Search3 == null || Search3 == "")
                    { }
                    else
                    {

                        print1 = print1 + 1;

                        int value1 = print1;
                        orderm.Quantity = Search3;
                        orderm.OrderID = value;
                        orderm.Name = "LongIsland";
                        orderm.PK = print1;

                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.Orders.Add(orderm);


                    }
                    if (Search4 == "0" || Search4 == null || Search4 == "")
                    { }
                    else
                    {

                        print1 = print1 + 1;

                        int value1 = print1;
                        ordermg.Quantity = Search4;
                        ordermg.OrderID = value;
                        ordermg.Name = "Martini";
                        ordermg.PK = print1;

                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.Orders.Add(ordermg);


                    }
                    if (Search5 == "0" || Search5 == null || Search5 == "")
                    { }
                    else
                    {

                        print1 = print1 + 1;

                        int value1 = print1;
                        orderp.Quantity = Search5;
                        orderp.OrderID = value;
                        orderp.Name = "PinaC";
                        orderp.PK = print1;

                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.Orders.Add(orderp);


                    }

                    dc.SaveChanges();
                    var v = dc.Customers.Where(a => a.OrderID.Equals(customer.OrderID)).FirstOrDefault();



                    Session["ID"] = v.OrderID.ToString();





                    return RedirectToAction("OrderDetail");
                }
                return View("Index");


            }
            return View("Index");
        }

        public ActionResult OrderDetail()
        {
            var CustOrderID = Session["ID"];

            int o = Convert.ToInt32(CustOrderID);

            List<Object> myModel = new List<Object>();

            var customer = from r in dc.Customers
                           where r.OrderID == o
                           select r;


            var order = from d in dc.Orders
                        where d.OrderID == o
                        select d;



            myModel.Add(customer.ToList());

            myModel.Add(order.ToList());
            //Get total for Customer
           int Baddition =0;
           int BLaddition=0;
          int  Paddition=0;
          int  MIaddition=0;
          int  MGaddition=0;
         int   LIaddition=0;

            var resultB =(order.ToList().Any(item => item.Name == "Baileys"));
            var resultBL = (order.ToList().Any(item => item.Name == "Blue"));
            var resultP = (order.ToList().Any(item => item.Name == "PinaC"));
            var resultM = (order.ToList().Any(item => item.Name == "Martini"));
            var resultMG = (order.ToList().Any(item => item.Name == "Margarita"));
            var resultLI = (order.ToList().Any(item => item.Name == "LongIsland"));


            if (resultB)
            {
                var BQuantity = (from d in dc.Orders
                                 where d.OrderID == o && d.Name == "Baileys"
                                 select d.Quantity).First();
                int BQ = Convert.ToInt32(BQuantity);

                Baddition = BQ * 8;
            }
            if (resultBL)
            {
                var BlQuantity = (from d in dc.Orders
                                  where d.OrderID == o && d.Name == "Blue"
                                  select d.Quantity).First();
                int BL = Convert.ToInt32(BlQuantity);

                BLaddition = BL * 10;
            }
            if (resultP)
            {
                var PQuantity = (from d in dc.Orders
                                 where d.OrderID == o && d.Name == "PinaC"
                                 select d.Quantity).First();
                int P = Convert.ToInt32(PQuantity);

                Paddition = P * 10;
            }
            if (resultM)
            {
                var mQuantity = (from d in dc.Orders
                                 where d.OrderID == o && d.Name == "Martini"
                                 select d.Quantity).First();
                int MI = Convert.ToInt32(mQuantity);

                MIaddition = MI * 9;
            }
            if (resultMG)
            {
                var mgQuantity = (from d in dc.Orders
                                  where d.OrderID == o && d.Name == "Margarita"
                                  select d.Quantity).First();
                int MG = Convert.ToInt32(mgQuantity);

                MGaddition = MG * 9;
            }
            if (resultLI)
            {
                var lQuantity = (from d in dc.Orders
                                 where d.OrderID == o && d.Name == "LongIsland"
                                 select d.Quantity).First();




                int LI = Convert.ToInt32(lQuantity);

                LIaddition = LI * 8;
            }
            int Total = Baddition + BLaddition + Paddition + MIaddition + MGaddition + LIaddition;
            ViewBag.Total = Total;
            return View(myModel.ToList());


        }
         

}



    

}