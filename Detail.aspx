<%@ Page Title="รายละเอียด-ลูกค้า" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Detail.aspx.cs" Inherits="Aioi.Detail" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">

        <div class="d-flex flex-column flex-md-row flex-lg-row" style="gap: 32px; flex-wrap: wrap">

            <div class="d-flex flex-column left ">
                <h3 class="header">ข้อมูลส่วนบุคคล</h3>

                <div class="photo-containner">
                    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="photo-outer">
                                <img id="img" runat="server" onclick="browseFile()" src="./images/nophoto.jpg" style="width: 200px; height: 200px; border-radius: 50%; cursor: pointer" class="photo" />
                                <input id="myFile" runat="server" type="file" accept="image/*" hidden onchange="updateImageAndPostBack(this)" />
                                <asp:Button ID="btnInputFile" runat="server" Style="display: none;" OnClick="btnInputFile_Click" />

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnInputFile" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>
            <div class="d-flex flex-column right">

                <div class=" d-flex flex-column flex-md-row flex-lg-row  flex-item-group">
                    <h3 id="divNewHeader" runat="server" class="header">เพิ่มข้อมูลใหม่</h3>


                </div>
                <div class=" d-flex flex-column flex-md-row flex-lg-row  flex-item-group">
                    <div class=" flex-column flex-item">
                        <label class="required">บัตรประชาชน:</label>
                        <asp:TextBox ID="txtSSID" runat="server" attern="\d{1}\-\d{4}\-\d{5}\-\d{2}\-\d{1}\" class="form-control id-card required"></asp:TextBox>

                    </div>
                    <div class=" flex-column flex-item item-dummy">
                        &nbsp;
                    </div>

                </div>


                <div class="d-flex flex-column flex-md-row flex-lg-row  flex-item-group">

                    <div class=" flex-column flex-item">
                        <label class="required">ชื่อ:</label>
                        <asp:TextBox ID="txtFirstname" runat="server" Width="100%" MaxLength="75" CssClass="form-control required"></asp:TextBox>
                    </div>
                    <div class=" flex-column flex-item">
                        <label class="required">สกุล:</label>
                        <asp:TextBox ID="txtLastname" runat="server" Width="100%" MaxLength="75" CssClass="form-control required"></asp:TextBox>
                    </div>

                </div>
                <div class="d-flex flex-column flex-md-row flex-lg-row  flex-item-group">
                    <div class=" flex-column  flex-item">
                        <label class="required">เพศ:</label>
                        <asp:DropDownList ID="ddlGender" CssClass="form-control required" Width="100%"
                            runat="server">
                            <asp:ListItem Value=""> -โปรดระบุเพศ- </asp:ListItem>
                            <asp:ListItem Value="F"> หญิง </asp:ListItem>
                            <asp:ListItem Value="M"> ชาย </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class=" flex-column  flex-item">
                        <label class="required">วันเดือนปี เกิด:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control required" Width="100%"
                                ReadOnly="true"
                                Style="cursor: pointer; width: 100%; max-width: 100%!important; border: 1px solid;"></asp:TextBox>
                            <span class="input-group-addon" style="cursor: pointer; position: absolute; transform: translateY(-50%); right: 5px; top: 50%;">
                                <i id="iCal" runat="server" class="far fa-calendar"></i>
                            </span>
                            <asp:HiddenField ID="hDate" runat="server" />
                        </div>

                    </div>
                </div>

                <div class="d-flex flex-column flex-md-row flex-lg-row flex-item-group">
                    <div class=" flex-column  flex-item">
                        <label>อีเมล:</label>
                        <asp:TextBox ID="txtEmail" runat="server" Width="100%" MaxLength="100" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class=" flex-column  flex-item">
                        <label class="required">เบอร์โทร:</label>
                        <asp:TextBox ID="txtPhone" runat="server" Width="100%" MaxLength="100" CssClass="form-control required"></asp:TextBox>
                    </div>
                </div>


                <div class="d-flex flex-column flex-md-row flex-lg-row  flex-item-group">
                    <div class=" flex-column  flex-item">
                        <label class="required">ที่ตั้ง:</label>
                        <asp:TextBox ID="txtAdd1" runat="server" CssClass="form-control" Width="100%" MaxLength="255"></asp:TextBox>
                    </div>

                </div>


                <asp:UpdatePanel ID="upState" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="d-flex flex-column flex-md-row flex-lg-row  flex-item-group">
                            <div class=" flex-column  flex-item">
                                <label class="required">จังหวัด:</label>
                                <asp:DropDownList ID="ddlState" CssClass="select form-control required" Width="100%"
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="onState_Change"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class=" flex-column  flex-item">
                                <label class="required">อำเภอ/เขต:</label>
                                <asp:DropDownList ID="ddlCity" CssClass="select form-control required" Width="100%" runat="server">
                                    <asp:ListItem Value=""> -โปรดระบุเขต/อำเภอ- </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlState" EventName="SelectedIndexChanged" />

                    </Triggers>
                </asp:UpdatePanel>

                <div class="d-flex flex-column flex-md-row flex-lg-row  flex-item-group">
                    <div class=" flex-column  flex-item">
                        <label>ตำบล/แขวง:</label>
                        <asp:TextBox ID="txtAdd2" runat="server" Width="100%" MaxLength="255" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class=" flex-column  flex-item">
                        <label class="required">รหัสไปรษณีย์:</label>
                        <asp:TextBox ID="txtZip" runat="server" Width="100%" MaxLength="5" CssClass="form-control required"></asp:TextBox>
                    </div>
                </div>
                <div class="d-flex flex-column flex-md-row flex-lg-row  flex-item-group"  style="align-items: center; justify-content: center;">

                    <asp:UpdatePanel ID="upErr" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="flex-row" style="justify-content: center; margin-top: 20px">
                                <asp:Label ID="lbErr" runat="server" Width="100%" ForeColor="Red"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="d-flex flex-column flex-md-row flex-lg-row  flex-item-group  " style="align-items: center; justify-content: center;">
                    <div class="flex-row" style="justify-content: center;  ">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSave" runat="server" Text="บันทึก" BackColor="#015bb5" OnClientClick="return checkMandatoryFields()" BorderColor="transparent" ForeColor="White" Width="70px"
                                    OnClick="btnSave_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="btnDelete" runat="server" Text="ลบ" BackColor="red" BorderColor="transparent" Visible="false"
                            OnClick="btnDelete_Click"
                            ForeColor="White" Width="70px"
                            OnClientClick="javascript: return confirm('ต้องการลบข้อมูล?');" />
                        <asp:Button ID="bacBack" runat="server" Text="ยกเลิก" BorderColor="transparent" Width="70px"
                            OnClick="btnBack_Click" />
                    </div>
                </div>


            </div>

        </div>

        <asp:UpdatePanel ID="upHiddenFile" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hImageFile" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>

    </main>
    <style>
        @media only screen and (max-width: 600px) {
            .photo-outer {
                display: flex;
                justify-content: center;
                align-items: center;
            }

            .photo-containner {
                align-self: center;
            }

            .right {
                flex-grow: 1;
                margin-top: 0px;
            }

            .item-dummy {
                display: none;
            }

            .left {
                flex-grow: 1;
                width: 100% !important;
                justify-content: center;
                width: 100% !important;
                max-width: 100% !important;
                align-items: flex-start !important;
            }

            .right {
                width: 100%;
            }

            .flex-item-group {
                width: 100%;
                gap: 16px;
                max-width: 100% !important;
            }
        }

        .header {
            margin-bottom: 20px;
            white-space: nowrap;
        }

        .left {
            width: 240px;
            align-items: center;
        }

        .right {
            flex-grow: 1;
        }



        .flex-item {
            width: 100%;
        }


        .flex-item-group {
            width: 100%;
            gap: 16px;
            max-width: 650px !important;
            margin-bottom: 12px;
        }

        .flex-item-full-width {
            width: 100%;
        }

        label.required::after {
            content: " *";
            color: red;
        }

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
        }

            .link-style a {
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
            height: 38px !important;
        }

        .photo-outer {
            position: relative;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#<%= txtDate.ClientID %>').datepicker({ dateFormat: 'yy-mm-dd', altField: '#<%= hDate.ClientID %>' });

            $('#<%= iCal.ClientID %>').click(function () {
                $('#<%= txtDate.ClientID %>').datepicker('show');
            })

            $('#<%= txtSSID.ClientID %>').mask('0-0000-00000-00-0', { autoclear: false });

            $('#<%= txtZip.ClientID %>').mask('00000', { autoclear: false });





            if ($('#<%= hDate.ClientID %>').val() != "") {

                $('#<%= txtDate.ClientID %>').val($('#<%= hDate.ClientID %>').val());
            }


        });

        function browseFile() {
            $('#<%= myFile.ClientID %>').click();
        }



        function updateImageAndPostBack(input) {
            var imgElement = document.getElementById('<%= img.ClientID %>');
            var btnInputFile = document.getElementById('<%= btnInputFile.ClientID %>');


            if (input.files && input.files[0]) {
                var maxFileSizeInBytes = 2 * 1024 * 1024; // 2 MB in bytes
                var fileSize = input.files[0].size; // File size in bytes

                if (fileSize > maxFileSizeInBytes) {
                    alert("File size exceeds the maximum allowed size (2 MB).");
                    // Optionally, clear the file input
                    input.value = "";
                } else {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#<%= hImageFile.ClientID %>').val(e.target.result);
                        imgElement.src = $('#<%= hImageFile.ClientID %>').val();

                        // Trigger postback using __doPostBack
                        __doPostBack('<%= btnInputFile.UniqueID %>', '');
                    };

                    reader.readAsDataURL(input.files[0]);
                }
            }
        }

        function checkMandatoryFields() {
            var fields = [

                { id: '<%= txtSSID.ClientID %>', message: "โปรดระบุ หมายเลขบัตรประชาชน" },
                { id: '<%= txtFirstname.ClientID %>', message: "โปรดระบุ ชื่อ" },
                { id: '<%= txtLastname.ClientID %>', message: "โปรดระบุ นามสกุล" },
                { id: '<%= ddlGender.ClientID %>', message: "โปรดระบุ เพศ" },
                { id: '<%= hDate.ClientID %>', message: "โปรดระบุ วันเดือนปี เกิด" },
                { id: '<%= txtPhone.ClientID %>', message: "โปรดระบุ เบอร์โทรศัพท์" },
                { id: '<%= txtAdd1.ClientID %>', message: "โปรดระบุ ที่อยู่" },
                { id: '<%= ddlState.ClientID %>', message: "โปรดระบุ จังหวัด" },
                { id: '<%= ddlCity.ClientID %>', message: "โปรดระบุ เขต/อำเภอ" },
                { id: '<%= txtZip.ClientID %>', message: "โปรดระบุ รหัสไปรษณีย์" }
            ];

            var result = true;
            var message = "";

            for (var i = 0; i < fields.length; i++) {
                var field = fields[i];
                var fieldValue = $('#' + field.id).val();

                if (fieldValue === "") {
                    message = field.message;
                    break;
                }
            }

            if (message !== "") {
                $('#<%= lbErr.ClientID %>').html(message);

                result = false;
            }

            return result;


        }


    </script>





</asp:Content>

