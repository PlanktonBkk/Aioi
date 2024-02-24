<%@ Page Title="รายละเอียด-ลูกค้า" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="Aioi.Detail" %>

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
            gap: 16px;
        }

        .box {
            margin-bottom: 40px;
        }

        .flex-col {
            display: flex;
            flex-direction: column;
            row-gap: 0px;
            margin-top: 10px;
        }

        .text-red {
            color: red;
        }

        .row-style {
            height: 38px;
            background-color: #fff;
        }

        .alternate-row-style {
            height: 38px;
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
            a

        {
            text-decoration: none;
        }



        legend {
            font-size: 1.25em;
            border-bottom: 0.25px solid lightgrey;
            margin-bottom: 0px;
        }

        .photo-box {
            margin-top: 25px !important;
        }

        .photo {
            width: 200px;
            height: 200px;
            margin-top: 16px;
        }

        .full-width {
            width: 100%;
            max-width: 100% !important;
        }

        .id-card {
            cursor: pointer;
        }

        input-group {
            position: relative;
            display: flex;
        }

        .input-group-addon {
            position: absolute;
            right: 4px;
            top: 2px;
            transform: translateY(-50%);
        }

        .date {
            width: 100%;
            max-width: 100% !important;
        }

        .select {
            height: 30px !important;
        }

        .photo-outer {
            position: relative;
        }
    </style>
    <main aria-labelledby="title">
        <div class="box">
            <div class="flex-row" style="gap: 32px;">
                <div class="photo-box">

                    <h3>ข้อมูลส่วนบุคคล</h3>
                    <div class="photo-outer">
                        <img id="img" runat="server" src="./images/photo2.jpg" style="width: 200px; height: 200px; border-radius: 50%;" class="photo"></img>
                    </div>


                </div>
                <div>
                    <div id="divNewHeader" runat="server" class="flex-col " style="margin-top: 0px">
                        <h3>เพิ่มข้อมูลใหม่</h3>
                    </div>
                    <div class="flex-col ">
                        <label>บัตรประชาชน:</label> 
                        <asp:TextBox ID="txtSSID" runat="server" attern="\d{1}\-\d{4}\-\d{5}\-\d{2}\-\d{1}\" class="form-control id-card" Width="50%"></asp:TextBox> 
                    </div>
                    <div class="flex-row">
                        <div class="flex-col">
                            <label>ชื่อ:</label>
                            <asp:TextBox ID="txtFirstname" runat="server" Width="355" MaxLength="75" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="flex-col">
                            <label>สกุล:</label>
                            <asp:TextBox ID="txtLastname" runat="server" Width="355" MaxLength="75" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="flex-row">
                        <div class="flex-col" style="width: 50%">
                            <label>เพศ:</label>
                            <asp:DropDownList ID="ddlGender" CssClass="form-control"
                                runat="server">
                                <asp:ListItem Value=""> -โปรดระบุเพศ- </asp:ListItem>
                                <asp:ListItem Value="F"> หญิง </asp:ListItem>
                                <asp:ListItem Value="M"> ชาย </asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <div class="flex-col" style="width: 50%">
                            <label>วันเดือนปี เกิด:</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                                    ReadOnly="true" EnableViewState="true"
                                    Style="cursor: pointer; width: 100%; max-width: 100%!important; border: 1px solid;"></asp:TextBox>
                                <span class="input-group-addon" style="cursor: pointer; position: absolute; transform: translateY(-50%); right: 5px; top: 50%;">
                                    <i id="iCal" runat="server" class="far fa-calendar"></i>
                                </span>
                                <asp:HiddenField ID="hDate" runat="server" />
                            </div>

                        </div>
                    </div>
                    <div class="flex-row">
                        <div class="flex-col">
                            <label>อีเมล:</label>
                            <asp:TextBox ID="txtEmail" runat="server" Width="355" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="flex-col">
                            <label>เบอร์โทร:</label>
                            <asp:TextBox ID="txtPhone" runat="server" Width="355" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <fieldset>

                        <legend style="height: 14px;">&nbsp;</legend>
                        <div class="flex-row">
                            <div class="flex-col" style="width: 100%">
                                <label>ที่ตั้ง:</label>
                                <asp:TextBox ID="txtAdd1" runat="server" CssClass="full-width form-control" Style="max-width: 100%!important" Width="100%" MaxLength="255"></asp:TextBox>
                            </div>

                        </div>
                        <div class="flex-row">
                            <div class="flex-col" style="width: 50%">
                                <label>จังหวัด:</label>
                                <asp:DropDownList ID="ddlState" CssClass="select form-control"
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="onState_Change"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="flex-col" style="width: 50%">
                                <label>อำเภอ/เขต:</label>
                                <asp:DropDownList ID="ddlCity" CssClass="select form-control" runat="server">
                                    <asp:ListItem Value=""> -โปรดระบุเขต/อำเภอ- </asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="flex-row">
                            <div class="flex-col">
                                <label>ตำบล/แขวง:</label>
                                <asp:TextBox ID="txtAdd2" runat="server" Width="355" MaxLength="255" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="flex-col">
                                <label>รหัสไปรษณีย์:</label>
                                <asp:TextBox ID="txtZip" runat="server" Width="355"  MaxLength="5" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <div class="flex-row" style="justify-content: center; margin-top: 20px;">
                        <asp:Label ID="lbErr" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="flex-row" style="justify-content: center; margin-top: 20px;">

                        <asp:Button ID="btnSave" runat="server" Text="บันทึก" BackColor="#015bb5" BorderColor="transparent" ForeColor="White" Width="70px"
                            OnClick="btnSave_Click" />
                  <asp:Button ID="btnDelete" runat="server" Text="ลบ" BackColor="red" BorderColor="transparent" 
                       OnClick="btnDelete_Click" 
                      ForeColor="White" Width="70px" 
                         OnClientClick="javascript: return confirm('ต้องการลบข้อมูล?');" />
                        <asp:Button ID="bacBack" runat="server" Text="ยกเลิก" BorderColor="transparent" Width="70px"
                            OnClick="btnBack_Click" />

                    </div>
                </div>
            </div>





        </div>

    </main>

    <script type="text/javascript">
        $(function () {
            $('#<%= txtDate.ClientID %>').datepicker({ dateFormat: 'yy-mm-dd', altField: '#<%= hDate.ClientID %>' }); 

            $('#<%= iCal.ClientID %>').click(function () {
                $('#<%= txtDate.ClientID %>').datepicker('show');
            }) 

            $('#<%= txtSSID.ClientID %>').mask('0-0000-00000-00-0', { autoclear: false }); 


            if ($('#<%= hDate.ClientID %>').val() != "") {

                $('#<%= txtDate.ClientID %>').val($('#<%= hDate.ClientID %>').val());

            } 



        });


    </script>





</asp:Content>

