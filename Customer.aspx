<%@ Page Title="ข้อมูลลูกค้า" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="Aioi.Customer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .flex-row {
            display: flex;
            flex-direction: row;
            gap: 16px;
        }

        .hint {
            font-size: 11px;
            color: forestgreen;
        }

        .condition {
            display: flex;
            flex-direction: row;
            gap: 8px;
        }

        .box {
            margin-bottom: 20px;
        }

        .flex-col {
            display: flex;
            flex-direction: column;
            gap: 10px;
        }

        .text-red {
            color: red;
        }

        .row-style {
            height: 50px;
            background-color: #fff;
        }

        .alternate-row-style {
            height: 50px;
            background-color: #f3f3f3;
        }

        .keyword {
            width: 355px;
            max-width: 355px;
           
        }

        .text-center {
            text-align: center !important;
        }

        .link-style {

            text-align: center;
      

                a{
                    text-decoration: none;
                }

        }
   
    </style>
    <main aria-labelledby="title">

        <h4><%: Title %></h4>
        <br />

        <div class="box">
            <div>
                <label>ค้นหา:</label>
            </div>
            <div class="condition">
                <asp:TextBox ID="txtKeyword" runat="server" Width="355"  CssClass="keyword  form-control "></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Height="38px" BackColor="#015bb5" BorderColor="transparent" ForeColor="White" Width="70px" OnClick="btnSearch_Click" />
                 <asp:Button ID="btnNew" runat="server" Text="เพิ่มรายชื่อลูกค้า" Height="38px" BackColor="#00954e" BorderColor="#00954e" ForeColor="White" Width="130px" OnClick="btnNew_Click" />
            </div>
            <div class="hint">
                ** ระบุบางส่วนของคำที่จะค้นหาใน บ้ตรประชาชน, ชื่อ, สกุล, อีเมล, เบอร์โทรศัพท์
            </div>
        </div>

        <div class="flex-col">
            <div class="flex-row">
                <labe id="Labe1" runat="server">ผลการค้นหา:</labe>
                <labe id="lbCount" runat="server" class="text-red">0</labe>
                <labe id="Labe2" runat="server">รายการ</labe>
            </div>
            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="false" Width="100%"
                BorderColor="Transparent"
                RowStyle-CssClass="gridview-hover" AlternatingRowStyle-CssClass="alternate-row-style">
                <Columns>
                    <asp:TemplateField HeaderText="ลำดับ" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="20px">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 + "." %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="~/detail.aspx?id={0}"
                        DataTextField="ssid" HeaderText="บัตรประชาชน" ItemStyle-Width="150px" ItemStyle-CssClass="link-style" HeaderStyle-CssClass="text-center"></asp:HyperLinkField>


                    <asp:TemplateField HeaderText="ชื่อ-สกุล" SortExpression="firstname" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <%# Eval("firstname") + " " + Eval("lastname") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เพศ" SortExpression="gender" ItemStyle-Width="40px">
                        <ItemTemplate>
                            <%# Eval("gender").ToString() == "M" ? "ชาย" : "หญิง" %>
                        </ItemTemplate>
                    </asp:TemplateField>


<asp:TemplateField HeaderText="วันเดือนปี เกิด" HeaderStyle-HorizontalAlign="Center"
    ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"
    HeaderStyle-Width="80px" ItemStyle-Width="180px">
    <ItemTemplate>
        <%# Eval("birthdate") != DBNull.Value ? Convert.ToDateTime(Eval("birthdate")).ToString("dd MMM yyyy") + "  (" + CalculateAge((DateTime)Eval("birthdate")) + " ปี)" : "N/A" %>
    </ItemTemplate>
</asp:TemplateField>


                    <asp:BoundField DataField="email" HeaderText="อีเมล" SortExpression="email" ItemStyle-Width="170px" />

                    <asp:BoundField DataField="phone" HeaderText="เบอร์โทร" SortExpression="phone" ItemStyle-Width="130px" />
                    <asp:TemplateField HeaderText="ที่อยู่" SortExpression="state">
                        <ItemTemplate>
                            <%# Eval("address_line1") + " " + Eval("address_line2")+ " " + Eval("city")+ " " + Eval("state")+ " " + Eval("zip") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>



            </asp:GridView>
        </div>


    </main>
</asp:Content>
