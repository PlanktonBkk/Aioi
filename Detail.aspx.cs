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
using Newtonsoft.Json.Linq;
using System.EnterpriseServices.Internal;
using System.Security.Claims;
using System.Web.Services.Description;
using Newtonsoft.Json;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Aioi
{ 
    public partial class Detail : Page
    {
     
        public string dateVal;
        public string _id;
        protected void Page_Load(object sender, EventArgs e)
        { 
            lbErr.Text = "";
            _id = HttpContext.Current.Request.QueryString["id"];
     

            if (!IsPostBack)
            {        
                setStateDropdown();
                ddlState.SelectedIndexChanged += new EventHandler(onState_Change);
                Page.ClientScript.RegisterForEventValidation(ddlState.UniqueID);
            }
        }

         
        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                fillPage();
                if (String.IsNullOrWhiteSpace(_id))
                {
                    btnDelete.Visible = false;
                    divNewHeader.Visible = true;
                  
                }
                else
                {
                    btnDelete.Visible = true;
                    divNewHeader.Visible = false; 
                }
            }

            fillCustomerImage();



        }



        protected override void OnSaveStateComplete(EventArgs e)
        { 
        }


        private void fillCustomerImage()
        {



            if (String.IsNullOrWhiteSpace(_id))
            {

                if (hImageFile.Value.Length > 0)
                {
                    img.Src = hImageFile.Value;
                }
                else
                {
                    img.Src = "./images/nophoto.jpg" + "?v=" + inFunction.getUnixDateTime();
                }

            }
            else
            {
                string uploadDirectory = Server.MapPath("~/uploads/");
                string fileName = _id + ".jpg";
                string filePath = Path.Combine(uploadDirectory, fileName);
                if (File.Exists(filePath))
                {
                    img.Src = "~/uploads/" + fileName + "?v=" + inFunction.getUnixDateTime();
                }
                else
                {
                    img.Src = "~/images/nophoto.jpg" + "?v=" + inFunction.getUnixDateTime();
                }
            }





            
            updatePanel.Update();
        }

        private void fillPage()
        { 
            using (SqlConnection conn = new SqlConnection(inFunction.getConnectionStr()))
            {
                conn.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" select * from customer where id = " + inFunction.getTxt(_id));
                using (SqlCommand cmd = new SqlCommand(null, conn))
                {
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = sql.ToString();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtSSID.Text = inFunction.CString(reader["ssid"]);
                        txtFirstname.Text = inFunction.CString(reader["firstname"]);
                        txtLastname.Text = inFunction.CString(reader["lastname"]);
                        txtPhone.Text = inFunction.CString(reader["phone"]);
                        txtEmail.Text = inFunction.CString(reader["email"]);
                        hDate.Value = inFunction.CString(reader["birthdate"], "yyyy-MM-dd");

                        txtAdd1.Text = inFunction.CString(reader["address_line1"]);
                        txtAdd2.Text = inFunction.CString(reader["address_line2"]);
                        txtZip.Text = inFunction.CString(reader["zip"]);
                        inFunction.SetValueToDropDown(ddlGender, inFunction.CString(reader["gender"]));
                        inFunction.SetValueToDropDown(ddlCity, inFunction.CString(reader["city"]));
                        inFunction.SetValueToDropDown(ddlState, inFunction.CString(reader["state"]));
                         

                    }
                    reader.Close();
                }
                conn.Close();

            }
        }
        protected void btnInputFile_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(_id))
            {
                saveFile();
            }
         
        }
         
        protected void saveFile()
        { 

            HttpFileCollection files = Request.Files;
   
            if (files.Count > 0)
            {
                HttpPostedFile postedFile = files[0]; 

                if (postedFile != null && postedFile.ContentLength > 0)
                { 
                  
                    
                        string uploadDirectory = Server.MapPath("~/uploads/");
                        string fileName = _id + ".jpg";
                        string filePath = Path.Combine(uploadDirectory, fileName);

                        if (File.Exists(filePath))
                        {

                            File.Delete(filePath);
                        } 

                        postedFile.SaveAs(filePath); 

                        img.Src = "~/uploads/" + fileName + "?v=" + inFunction.getUnixDateTime(); 
                     
                }
            }
        }
         

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string newId = "";

            using (SqlConnection conn = new SqlConnection(inFunction.getConnectionStr()))
            {
                conn.Open();
                StringBuilder sql = new StringBuilder();

                using (SqlCommand cmd = new SqlCommand(null, conn))
                {
                    cmd.CommandTimeout = 300;  
                    string ssid = txtSSID.Text.Replace("-", "");
                  
                     
                    if (String.IsNullOrWhiteSpace(_id))
                    { 
                        cmd.CommandText = " select ssid from customer where ssid = " + inFunction.getTxt(ssid);
                        if(inFunction.CString(cmd.ExecuteScalar()) != "")
                        { 
                            lbErr.Text = "บันทึกข้อมูลไม่สำเร็จ มีข้อมูลบัตรประชาชนนี่อยู่แล้ว"; 
                        }else if (String.IsNullOrWhiteSpace(ssid))
                        {
                            lbErr.Text = "บันทึกข้อมูลไม่สำเร็จ โปรดระบุหมายเลขบัตรประชาชน";
                        }
                        else
                        {
                            newId = System.Guid.NewGuid().ToString().ToUpper();

                            sql.AppendLine(" insert into customer (id, ssid, firstname, lastname, gender, birthdate, address_line1, address_line2, city , state , zip, phone, email) ");
                            sql.AppendLine(" values( " + inFunction.getSqlTxt(newId));
                            sql.AppendLine(" , " + inFunction.getSqlTxt(ssid));
                            sql.AppendLine(" , " + inFunction.getSqlTxt(txtFirstname.Text));
                            sql.AppendLine(" , " + inFunction.getSqlTxt(txtLastname.Text));
                            sql.AppendLine(" , " + inFunction.getSqlTxt(ddlGender.SelectedValue));
                            sql.AppendLine(" , " + inFunction.getSqlTxt(inFunction.getDateTxt(hDate.Value)));
                            sql.AppendLine(" , " + inFunction.getSqlTxt(txtAdd1.Text));
                            sql.AppendLine(" , " + inFunction.getSqlTxt(txtAdd2.Text)); 
                            sql.AppendLine(" , " + inFunction.getSqlTxt(ddlCity.SelectedValue) ); 
                            sql.AppendLine(" , " + inFunction.getSqlTxt(ddlState.SelectedValue) ); 
                            sql.AppendLine(" , " + inFunction.getSqlTxt(txtZip.Text));
                            sql.AppendLine(" , " + inFunction.getSqlTxt(txtPhone.Text));
                            sql.AppendLine(" , " + inFunction.getSqlTxt(txtEmail.Text));
                            sql.AppendLine(" ); ");
                        } 
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(ssid))
                        {
                            lbErr.Text = "บันทึกข้อมูลไม่สำเร็จ โปรดระบุหมายเลขบัตรประชาชน";
                        }
                        else
                        {
                            sql.AppendLine(" update customer ");
                            sql.AppendLine(" set ssid = " + inFunction.getSqlTxt(ssid));
                            sql.AppendLine(" , firstname = " + inFunction.getSqlTxt(txtFirstname.Text));
                            sql.AppendLine(" , lastname = " + inFunction.getSqlTxt(txtLastname.Text));
                            sql.AppendLine(" , gender = " + inFunction.getSqlTxt(ddlGender.SelectedValue));
                            sql.AppendLine(" , birthdate = " + inFunction.getSqlTxt(inFunction.getDateTxt(hDate.Value)));
                            sql.AppendLine(" , address_line1 = " + inFunction.getSqlTxt(txtAdd1.Text));
                            sql.AppendLine(" , address_line2 = " + inFunction.getSqlTxt(txtAdd2.Text));

                           sql.AppendLine(" , city = " +   inFunction.getSqlTxt(ddlCity.SelectedValue) );
                            sql.AppendLine(" , state = " +  inFunction.getSqlTxt(ddlState.SelectedValue) );

                            sql.AppendLine(" , zip = " + inFunction.getSqlTxt(txtZip.Text));
                            sql.AppendLine(" , phone = " + inFunction.getSqlTxt(txtPhone.Text));
                            sql.AppendLine(" , email = " + inFunction.getSqlTxt(txtEmail.Text));
                            sql.AppendLine(" where id = " + inFunction.getSqlTxt(_id));
                        }
                      
                    }

                     
                    upErr.Update();
                    string sqlStr = sql.ToString();
                    if (sqlStr != "")
                    { 
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();


                        if (!String.IsNullOrWhiteSpace(newId) && hImageFile.Value.Length > 0)
                        {

                            string[] imgString = hImageFile.Value.Split(new string[] { "base64," }, StringSplitOptions.None);

                            if (imgString.Length == 2)
                            {
                                string base64Image = imgString[1];

                                byte[] imageBytes = Convert.FromBase64String(base64Image);
                                string uploadDirectory = Server.MapPath("~/uploads/");
                                string fileName = newId + ".jpg";
                                string filePath = Path.Combine(uploadDirectory, fileName);
                                if (File.Exists(filePath))
                                {
                                    File.Delete(filePath);
                                }
                                File.WriteAllBytes(filePath, imageBytes);

                            } 
                        } 

                        HttpContext.Current.Response.Redirect("/customer.aspx");
                    }
                   

                }
                conn.Close();

            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("/Customer.aspx", true);

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(inFunction.getConnectionStr()))
            {
                conn.Open(); 
                using (SqlCommand cmd = new SqlCommand(null, conn))
                {
                    cmd.CommandTimeout = 300; 
                    if (!String.IsNullOrWhiteSpace(_id))
                    { 
                        cmd.CommandText = " delete from customer where id = " + inFunction.getTxt(_id);
                        cmd.ExecuteNonQuery();
                        HttpContext.Current.Response.Redirect("/customer.aspx");
                    } 
                }
                conn.Close(); 
            }

        }



        protected void onState_Change(object sender, EventArgs e)
        {
            string[] cities = inFunction.getCities(ddlState.SelectedValue.ToString());

            var cityList = new ListItem[] { new ListItem("-โปรดระบุเขต/อำเภอ-", "") }
                .Concat(cities.Select(state => new ListItem(state, state)))
                .ToArray();

            ddlCity.DataSource = cityList;
            ddlCity.DataBind();
            ddlCity.Items[0].Value = "";
            ddlCity.SelectedIndex = 0;
            upState.Update();
        }




        protected void onCity_Change(object sender, EventArgs e)
        {


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



        private void setStateDropdown()
        {

            string[] states = inFunction.getStates();

            var stateList = new ListItem[] { new ListItem("-โปรดระบุจังหวัด-", "") }
                .Concat(states.Select(state => new ListItem(state, state)))
                .ToArray();
            ddlState.DataSource = stateList; 
            ddlState.DataBind();
            ddlState.Items[0].Value = "";  
            ddlState.SelectedIndex = 0;

        }


    }

}