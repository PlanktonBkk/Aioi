using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Collections;

namespace Aioi
{
    public partial class Customer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
       
                BindGridView();
            }
        }

        private void BindGridView()
        {
         
            string query = "SELECT id, ssid, firstname, lastname, gender, birthdate, address_line1, address_line2, city, state, zip, phone, email FROM customer";

           
            using (SqlConnection connection = new SqlConnection(inFunction.getConnectionStr()))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                
                DataSet dataSet = new DataSet();

             
                adapter.Fill(dataSet);

             
                GridView1.DataSource = dataSet;

                
                GridView1.DataBind();
                lbCount.InnerText = GridView1.Rows.Count.ToString();
            }
        }

     

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("/detail", true);
        }


        protected void btnSearch_Click(object sender, EventArgs e) 
        {
       
            BindGridViewWithSearch();
        }





        private void BindGridViewWithSearch()
        {


            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select top 200 id, ssid, firstname, lastname, gender, birthdate, address_line1, address_line2, city, state, zip, phone, email ");
            sql.AppendLine(" from customer ");
            sql.AppendLine(" where 1=1 ");
            if (txtKeyword.Text.Trim() != "")
            {
                sql.AppendLine(" and ( "); 
                sql.AppendLine(" ssid LIKE '%' + @ssid + '%' ");
                sql.AppendLine(" or firstname LIKE '%' + @firstname + '%' ");
                sql.AppendLine(" or lastname LIKE '%' + @lastname + '%' ");
                sql.AppendLine(" or gender LIKE '%' + @gender + '%' ");
                sql.AppendLine(" or phone LIKE '%' + @phone + '%' ");
                sql.AppendLine(" or email LIKE '%' + @email + '%' ");
                sql.AppendLine(" ) ");
            }
             

            using (SqlConnection connection = new SqlConnection(inFunction.getConnectionStr()))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql.ToString(), connection);
                if (txtKeyword.Text.Trim() != "")
                {

                    adapter.SelectCommand.Parameters.AddWithValue("@ssid", txtKeyword.Text);
                    adapter.SelectCommand.Parameters.AddWithValue("@firstname", txtKeyword.Text);
                    adapter.SelectCommand.Parameters.AddWithValue("@lastname", txtKeyword.Text);
                    adapter.SelectCommand.Parameters.AddWithValue("@gender", txtKeyword.Text);
                    adapter.SelectCommand.Parameters.AddWithValue("@phone", txtKeyword.Text);
                    adapter.SelectCommand.Parameters.AddWithValue("@email", txtKeyword.Text);
    

                }


                DataSet dataSet = new DataSet();
                 
                adapter.Fill(dataSet); 
        
                GridView1.DataSource = dataSet;  
                GridView1.DataBind();

                lbCount.InnerText =   GridView1.Rows.Count.ToString()  ;

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#fffbf4';this.style.cursor='pointer';");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=this.originalstyle;");

                   
                }
            }
        }
     
        protected int CalculateAge(DateTime birthdate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthdate.Year; 
            if (birthdate.Date > currentDate.AddYears(-age))
            {
                age--;
            }

            return age;
        }

      
    }

}