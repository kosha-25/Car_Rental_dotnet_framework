using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Car_Rental.View.Admin
{
    public partial class Rents : System.Web.UI.Page
    {
        Models.Functions Conn;
        public override void VerifyRenderingInServerForm(Control control)
        {
            base.VerifyRenderingInServerForm(control);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new Models.Functions();
            ShowRents();
        }

        private void ShowRents()
        {

            string Query = "select * from RentTb1";
            RentList.DataSource = Conn.GetData(Query);
            RentList.DataBind();
        }
       

        private void UpdateCar()
        {
            try
            {

                string Status = "Available";
                string Query = "update CarTb1 set  Status = '{0}' where CplateNum = '{1}'";
                Query = String.Format(Query, Status, RentList.SelectedRow.Cells[2].Text);

                Conn.SetData(Query);
                //ShowRents();
                //  ErrorMsg.InnerText = "Car Edited";
            }
            catch (Exception Ex)
            {
                
                InfoMsg.InnerText = Ex.Message;
            }
        }

        private void ReturnCar()
        {
            try
            {
              
                    string Query = "Delete from RentTb1 where RentId={0}";
                    Query = String.Format(Query, RentList.SelectedRow.Cells[1].Text);

                    Conn.SetData(Query);

                ShowRents();

            }
            catch (Exception)
            {
                throw;
            }
            
        }

        string LicensePlate;
        protected void RentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LicensePlate = RentList.SelectedRow.Cells[1].Text;
        }

        

        protected void BookBtn_Click1(object sender, EventArgs e)
        {
            try
            {
                if (RentList.SelectedRow.Cells[1].Text == "")
                {
                    InfoMsg.InnerText = "Select a Rental";
                }
                else
                {

                    string Query = "insert into ReturnTb1 values('{0}', {1}, '{2}', {3}, {4})";
                    Query = String.Format(Query, RentList.SelectedRow.Cells[2].Text, RentList.SelectedRow.Cells[3].Text, DateTime.Now.ToString("dd-MM-yyyy"), DelayTb.Value, FineTb.Value);
                    UpdateCar();
                    ShowRents();
                    Conn.SetData(Query);
                    
                    ReturnCar();
                    InfoMsg.InnerText = "Car Returned";
                    
                }
            }
            catch (Exception Ex)
            {
                // throw;
                InfoMsg.InnerText = Ex.Message;
            }
            DelayTb.Value = string.Empty;
            FineTb.Value = string.Empty;
        }
    }
}