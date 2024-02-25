﻿using System;
using System.Configuration;
using System.Resources;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;

namespace Aioi
{

    public class CityData
    {
        public string state { get; set; }
        public string city { get; set; }
    }

    internal class inFunction
    {
        public static string CString(Object obj, String DateFormat = null, String NumFormat = null)
        {
            if (obj == null || obj == DBNull.Value || (obj.GetType() == typeof(String) && String.IsNullOrEmpty((String)obj)))
            {
                if (String.IsNullOrEmpty(NumFormat))
                {
                    return "";
                }
                else
                {
                    return 0.ToString(NumFormat);
                }
            }
            else
            {
                if (obj.GetType() == typeof(Guid))
                {
                    return Convert.ToString(obj).ToUpper();
                }
                else if (obj.GetType() == typeof(Byte[]))
                {
                    return Convert.ToBase64String((Byte[])obj);
                }
                else if (obj.GetType() == typeof(DateTime))
                {
                    if (String.IsNullOrEmpty(DateFormat))
                    {
                        return ((DateTime)obj).ToShortDateString();
                    }
                    else
                    {
                        return ((DateTime)obj).ToString(DateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    }
                }
                else if (obj.GetType() == typeof(DateTimeOffset))
                {
                    if (String.IsNullOrEmpty(DateFormat))
                    {
                        return ((DateTimeOffset)obj).ToString("d");
                    }
                    else
                    {
                        return ((DateTimeOffset)obj).ToString(DateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    }
                }
                else if (!String.IsNullOrEmpty(NumFormat))
                {
                    try
                    {
                        double d = Convert.ToDouble(obj);
                        return d.ToString(NumFormat);
                    }
                    catch (Exception e)
                    {
                        return 0.ToString(NumFormat);
                    }
                }
                return Convert.ToString(obj);
            }
        }
        public static string getUnixDateTime()
        {

            Int32 unixTimestamp = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
            return inFunction.CString(unixTimestamp);

        }

        public static string getSqlTxt(string txt )
        {
            if (!String.IsNullOrEmpty(txt)) txt = txt.Trim();
            if (String.IsNullOrEmpty(txt)) return "null";
            return "'" + txt.Replace("'", "''") + "'";
        }

        public static string getTxt(object obj )
        {
            return getSqlTxt(CString(obj) );
        }

        public static string getDateTxt(string dateTimeString)
        {
        
            DateTime resultDateTime;
            if (String.IsNullOrEmpty(dateTimeString))
            {
                return  null;
            }
            else
            {
                resultDateTime = DateTime.Parse(dateTimeString); 
                string format = "yyyy-MM-dd";
                resultDateTime = DateTime.ParseExact(dateTimeString, format, CultureInfo.InvariantCulture); 
                return CString(resultDateTime, "yyyy-MM-dd");

            }
           




        }



  



        public static string getConfigVal(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static string getConnectionStr()
        {
            return ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        }

        public static void SetValueToDropDown(DropDownList dObject, string itemValue, string DisplayText = null, int optFind = 0)
        {
            itemValue = itemValue.Trim();
            DisplayText = DisplayText ?? itemValue;
            DisplayText = DisplayText.Trim();

            ListItem selectedLI = null;

            switch (optFind)
            {
                case 0:
                    for (int i = 0; i < dObject.Items.Count; i++)
                    {
                        if (string.Compare(dObject.Items[i].Value, itemValue, true) == 0)
                        {
                            selectedLI = dObject.Items[i];
                            break;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < dObject.Items.Count; i++)
                    {
                        if (string.Compare(dObject.Items[i].Text, DisplayText, true) == 0)
                        {
                            selectedLI = dObject.Items[i];
                            break;
                        }
                    }
                    break;
                default:
                    for (int i = 0; i < dObject.Items.Count; i++)
                    {
                        if (string.Compare(dObject.Items[i].Value, itemValue, true) == 0 &&
                            string.Compare(dObject.Items[i].Text, DisplayText, true) == 0)
                        {
                            selectedLI = dObject.Items[i];
                            break;
                        }
                    }
                    break;
            }

            // Add if not exists
            if (selectedLI == null)
            {
                selectedLI = new ListItem(DisplayText, itemValue);
                dObject.Items.Add(selectedLI);
            }

            dObject.SelectedIndex = dObject.Items.IndexOf(selectedLI);
        }

        public static string[] getCities(string paramState)
        {
            CityData[] CityArrayData = new CityData[]
            {
                   new CityData { state = "กรุงเทพมหานคร", city = "คลองเตย"},
  new CityData { state = "กรุงเทพมหานคร", city = "คลองสาน"},
  new CityData { state = "กรุงเทพมหานคร", city = "คลองสามวา"},
  new CityData { state = "กรุงเทพมหานคร", city = "คันนายาว"},
  new CityData { state = "กรุงเทพมหานคร", city = "จตุจักร"},
  new CityData { state = "กรุงเทพมหานคร", city = "จอมทอง"},
  new CityData { state = "กรุงเทพมหานคร", city = "ดอนเมือง"},
  new CityData { state = "กรุงเทพมหานคร", city = "ดินแดง"},
  new CityData { state = "กรุงเทพมหานคร", city = "ดุสิต"},
  new CityData { state = "กรุงเทพมหานคร", city = "ตลิ่งชัน"},
  new CityData { state = "กรุงเทพมหานคร", city = "ทวีวัฒนา"},
  new CityData { state = "กรุงเทพมหานคร", city = "ทุ่งครุ"},
  new CityData { state = "กรุงเทพมหานคร", city = "ธนบุรี"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางกอกน้อย"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางกอกใหญ่"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางกะปิ"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางขุนเทียน"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางเขน"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางคอแหลม"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางแค"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางซื่อ"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางนา"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางบอน"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางพลัด"},
  new CityData { state = "กรุงเทพมหานคร", city = "บางรัก"},
  new CityData { state = "กรุงเทพมหานคร", city = "บึงกุ่ม"},
  new CityData { state = "กรุงเทพมหานคร", city = "ปทุมวัน"},
  new CityData { state = "กรุงเทพมหานคร", city = "ประเวศ"},
  new CityData { state = "กรุงเทพมหานคร", city = "ป้อมปราบศัตรูพ่าย"},
  new CityData { state = "กรุงเทพมหานคร", city = "พญาไท"},
  new CityData { state = "กรุงเทพมหานคร", city = "พระโขนง"},
  new CityData { state = "กรุงเทพมหานคร", city = "พระนคร"},
  new CityData { state = "กรุงเทพมหานคร", city = "ภาษีเจริญ"},
  new CityData { state = "กรุงเทพมหานคร", city = "มีนบุรี"},
  new CityData { state = "กรุงเทพมหานคร", city = "ยานนาวา"},
  new CityData { state = "กรุงเทพมหานคร", city = "ราชเทวี"},
  new CityData { state = "กรุงเทพมหานคร", city = "ราษฎร์บูรณะ"},
  new CityData { state = "กรุงเทพมหานคร", city = "ลาดกระบัง"},
  new CityData { state = "กรุงเทพมหานคร", city = "ลาดพร้าว"},
  new CityData { state = "กรุงเทพมหานคร", city = "วังทองหลาง"},
  new CityData { state = "กรุงเทพมหานคร", city = "วัฒนา"},
  new CityData { state = "กรุงเทพมหานคร", city = "สวนหลวง"},
  new CityData { state = "กรุงเทพมหานคร", city = "สะพานสูง"},
  new CityData { state = "กรุงเทพมหานคร", city = "สัมพันธวงศ์"},
  new CityData { state = "กรุงเทพมหานคร", city = "สาทร"},
  new CityData { state = "กรุงเทพมหานคร", city = "สายไหม"},
  new CityData { state = "กรุงเทพมหานคร", city = "หนองแขม"},
  new CityData { state = "กรุงเทพมหานคร", city = "หนองจอก"},
  new CityData { state = "กรุงเทพมหานคร", city = "หลักสี่"},
  new CityData { state = "กรุงเทพมหานคร", city = "ห้วยขวาง"},
  new CityData { state = "กระบี่", city = "เกาะลันตา"},
  new CityData { state = "กระบี่", city = "เขาพนม"},
  new CityData { state = "กระบี่", city = "คลองท่อม"},
  new CityData { state = "กระบี่", city = "ปลายพระยา"},
  new CityData { state = "กระบี่", city = "เมืองกระบี่"},
  new CityData { state = "กระบี่", city = "ลำทับ"},
  new CityData { state = "กระบี่", city = "เหนือคลอง"},
  new CityData { state = "กระบี่", city = "อ่าวลึก"},
  new CityData { state = "กาญจนบุรี", city = "ด่านมะขามเตี้ย"},
  new CityData { state = "กาญจนบุรี", city = "ทองผาภูมิ"},
  new CityData { state = "กาญจนบุรี", city = "ท่าม่วง"},
  new CityData { state = "กาญจนบุรี", city = "ท่ามะกา"},
  new CityData { state = "กาญจนบุรี", city = "ไทรโยค"},
  new CityData { state = "กาญจนบุรี", city = "บ่อพลอย"},
  new CityData { state = "กาญจนบุรี", city = "พนมทวน"},
  new CityData { state = "กาญจนบุรี", city = "เมืองกาญจนบุรี"},
  new CityData { state = "กาญจนบุรี", city = "เลาขวัญ"},
  new CityData { state = "กาญจนบุรี", city = "ศรีสวัสดิ์"},
  new CityData { state = "กาญจนบุรี", city = "สังขละบุรี"},
  new CityData { state = "กาญจนบุรี", city = "หนองปรือ"},
  new CityData { state = "กาญจนบุรี", city = "ห้วยกระเจา"},
  new CityData { state = "กาฬสินธุ์", city = "กมลาไสย"},
  new CityData { state = "กาฬสินธุ์", city = "กุฉินารายณ์"},
  new CityData { state = "กาฬสินธุ์", city = "เขาวง"},
  new CityData { state = "กาฬสินธุ์", city = "คำม่วง"},
  new CityData { state = "กาฬสินธุ์", city = "ฆ้องชัย"},
  new CityData { state = "กาฬสินธุ์", city = "ดอนจาน"},
  new CityData { state = "กาฬสินธุ์", city = "ท่าคันโท"},
  new CityData { state = "กาฬสินธุ์", city = "นาคู"},
  new CityData { state = "กาฬสินธุ์", city = "นามน"},
  new CityData { state = "กาฬสินธุ์", city = "เมืองกาฬสินธุ์"},
  new CityData { state = "กาฬสินธุ์", city = "ยางตลาด"},
  new CityData { state = "กาฬสินธุ์", city = "ร่องคำ"},
  new CityData { state = "กาฬสินธุ์", city = "สมเด็จ"},
  new CityData { state = "กาฬสินธุ์", city = "สหัสขันธ์"},
  new CityData { state = "กาฬสินธุ์", city = "สามชัย"},
  new CityData { state = "กาฬสินธุ์", city = "หนองกุงศรี"},
  new CityData { state = "กาฬสินธุ์", city = "ห้วยผึ้ง"},
  new CityData { state = "กาฬสินธุ์", city = "ห้วยเม็ก"},
  new CityData { state = "กำแพงเพชร", city = "โกสัมพีนคร"},
  new CityData { state = "กำแพงเพชร", city = "ขาณุวรลักษบุรี"},
  new CityData { state = "กำแพงเพชร", city = "คลองขลุง"},
  new CityData { state = "กำแพงเพชร", city = "คลองลาน"},
  new CityData { state = "กำแพงเพชร", city = "ทรายทองวัฒนา"},
  new CityData { state = "กำแพงเพชร", city = "ไทรงาม"},
  new CityData { state = "กำแพงเพชร", city = "บึงสามัคคี"},
  new CityData { state = "กำแพงเพชร", city = "ปางศิลาทอง"},
  new CityData { state = "กำแพงเพชร", city = "พรานกระต่าย"},
  new CityData { state = "กำแพงเพชร", city = "เมืองกำแพงเพชร"},
  new CityData { state = "กำแพงเพชร", city = "ลานกระบือ"},
  new CityData { state = "ขอนแก่น", city = "กระนวน"},
  new CityData { state = "ขอนแก่น", city = "เขาสวนกวาง"},
  new CityData { state = "ขอนแก่น", city = "โคกโพธิ์ไชย"},
  new CityData { state = "ขอนแก่น", city = "ชนบท"},
  new CityData { state = "ขอนแก่น", city = "ชุมแพ"},
  new CityData { state = "ขอนแก่น", city = "ซำสูง"},
  new CityData { state = "ขอนแก่น", city = "น้ำพอง"},
  new CityData { state = "ขอนแก่น", city = "โนนศิลา"},
  new CityData { state = "ขอนแก่น", city = "บ้านไผ่"},
  new CityData { state = "ขอนแก่น", city = "บ้านฝาง"},
  new CityData { state = "ขอนแก่น", city = "บ้านแฮด"},
  new CityData { state = "ขอนแก่น", city = "เปือยน้อย"},
  new CityData { state = "ขอนแก่น", city = "พระยืน"},
  new CityData { state = "ขอนแก่น", city = "พล"},
  new CityData { state = "ขอนแก่น", city = "ภูผาม่าน"},
  new CityData { state = "ขอนแก่น", city = "ภูเวียง"},
  new CityData { state = "ขอนแก่น", city = "มัญจาคีรี"},
  new CityData { state = "ขอนแก่น", city = "เมืองขอนแก่น"},
  new CityData { state = "ขอนแก่น", city = "เวียงเก่า"},
  new CityData { state = "ขอนแก่น", city = "แวงน้อย"},
  new CityData { state = "ขอนแก่น", city = "แวงใหญ่"},
  new CityData { state = "ขอนแก่น", city = "สีชมพู"},
  new CityData { state = "ขอนแก่น", city = "หนองนาคำ"},
  new CityData { state = "ขอนแก่น", city = "หนองเรือ"},
  new CityData { state = "ขอนแก่น", city = "หนองสองห้อง"},
  new CityData { state = "ขอนแก่น", city = "อุบลรัตน์"},
  new CityData { state = "จันทบุรี", city = "แก่งหางแมว"},
  new CityData { state = "จันทบุรี", city = "ขลุง"},
  new CityData { state = "จันทบุรี", city = "เขาคิชฌกูฏ"},
  new CityData { state = "จันทบุรี", city = "ท่าใหม่"},
  new CityData { state = "จันทบุรี", city = "นายายอาม"},
  new CityData { state = "จันทบุรี", city = "โป่งน้ำร้อน"},
  new CityData { state = "จันทบุรี", city = "มะขาม"},
  new CityData { state = "จันทบุรี", city = "เมืองจันทบุรี"},
  new CityData { state = "จันทบุรี", city = "สอยดาว"},
  new CityData { state = "จันทบุรี", city = "แหลมสิงห์"},
  new CityData { state = "ฉะเชิงเทรา", city = "คลองเขื่อน"},
  new CityData { state = "ฉะเชิงเทรา", city = "ท่าตะเกียบ"},
  new CityData { state = "ฉะเชิงเทรา", city = "บางคล้า"},
  new CityData { state = "ฉะเชิงเทรา", city = "บางน้ำเปรี้ยว"},
  new CityData { state = "ฉะเชิงเทรา", city = "บางปะกง"},
  new CityData { state = "ฉะเชิงเทรา", city = "บ้านโพธิ์"},
  new CityData { state = "ฉะเชิงเทรา", city = "แปลงยาว"},
  new CityData { state = "ฉะเชิงเทรา", city = "พนมสารคาม"},
  new CityData { state = "ฉะเชิงเทรา", city = "เมืองฉะเชิงเทรา"},
  new CityData { state = "ฉะเชิงเทรา", city = "ราชสาส์น"},
  new CityData { state = "ฉะเชิงเทรา", city = "สนามชัยเขต"},
  new CityData { state = "ชลบุรี", city = "เกาะจันทร์"},
  new CityData { state = "ชลบุรี", city = "เกาะสีชัง"},
  new CityData { state = "ชลบุรี", city = "บ่อทอง"},
  new CityData { state = "ชลบุรี", city = "บางละมุง"},
  new CityData { state = "ชลบุรี", city = "บ้านบึง"},
  new CityData { state = "ชลบุรี", city = "พนัสนิคม"},
  new CityData { state = "ชลบุรี", city = "พานทอง"},
  new CityData { state = "ชลบุรี", city = "เมืองชลบุรี"},
  new CityData { state = "ชลบุรี", city = "ศรีราชา"},
  new CityData { state = "ชลบุรี", city = "สัตหีบ"},
  new CityData { state = "ชลบุรี", city = "หนองใหญ่"},
  new CityData { state = "ชัยนาท", city = "เนินขาม"},
  new CityData { state = "ชัยนาท", city = "มโนรมย์"},
  new CityData { state = "ชัยนาท", city = "เมืองชัยนาท"},
  new CityData { state = "ชัยนาท", city = "วัดสิงห์"},
  new CityData { state = "ชัยนาท", city = "สรรคบุรี"},
  new CityData { state = "ชัยนาท", city = "สรรพยา"},
  new CityData { state = "ชัยนาท", city = "หนองมะโมง"},
  new CityData { state = "ชัยนาท", city = "หันคา"},
  new CityData { state = "ชัยภูมิ", city = "เกษตรสมบูรณ์"},
  new CityData { state = "ชัยภูมิ", city = "แก้งคร้อ"},
  new CityData { state = "ชัยภูมิ", city = "คอนสวรรค์"},
  new CityData { state = "ชัยภูมิ", city = "คอนสาร"},
  new CityData { state = "ชัยภูมิ", city = "จัตุรัส"},
  new CityData { state = "ชัยภูมิ", city = "ซับใหญ่"},
  new CityData { state = "ชัยภูมิ", city = "เทพสถิต"},
  new CityData { state = "ชัยภูมิ", city = "เนินสง่า"},
  new CityData { state = "ชัยภูมิ", city = "บ้านเขว้า"},
  new CityData { state = "ชัยภูมิ", city = "บ้านแท่น"},
  new CityData { state = "ชัยภูมิ", city = "บำเหน็จณรงค์"},
  new CityData { state = "ชัยภูมิ", city = "ภักดีชุมพล"},
  new CityData { state = "ชัยภูมิ", city = "ภูเขียว"},
  new CityData { state = "ชัยภูมิ", city = "เมืองชัยภูมิ"},
  new CityData { state = "ชัยภูมิ", city = "หนองบัวแดง"},
  new CityData { state = "ชัยภูมิ", city = "หนองบัวระเหว"},
  new CityData { state = "ชุมพร", city = "ท่าแซะ"},
  new CityData { state = "ชุมพร", city = "ทุ่งตะโก"},
  new CityData { state = "ชุมพร", city = "ปะทิว"},
  new CityData { state = "ชุมพร", city = "พะโต๊ะ"},
  new CityData { state = "ชุมพร", city = "เมืองชุมพร"},
  new CityData { state = "ชุมพร", city = "ละแม"},
  new CityData { state = "ชุมพร", city = "สวี"},
  new CityData { state = "ชุมพร", city = "หลังสวน"},
  new CityData { state = "เชียงราย", city = "ขุนตาล"},
  new CityData { state = "เชียงราย", city = "เชียงของ"},
  new CityData { state = "เชียงราย", city = "เชียงแสน"},
  new CityData { state = "เชียงราย", city = "ดอยหลวง"},
  new CityData { state = "เชียงราย", city = "เทิง"},
  new CityData { state = "เชียงราย", city = "ป่าแดด"},
  new CityData { state = "เชียงราย", city = "พญาเม็งราย"},
  new CityData { state = "เชียงราย", city = "พาน"},
  new CityData { state = "เชียงราย", city = "เมืองเชียงราย"},
  new CityData { state = "เชียงราย", city = "แม่จัน"},
  new CityData { state = "เชียงราย", city = "แม่ฟ้าหลวง"},
  new CityData { state = "เชียงราย", city = "แม่ลาว"},
  new CityData { state = "เชียงราย", city = "แม่สรวย"},
  new CityData { state = "เชียงราย", city = "แม่สาย"},
  new CityData { state = "เชียงราย", city = "เวียงแก่น"},
  new CityData { state = "เชียงราย", city = "เวียงชัย"},
  new CityData { state = "เชียงราย", city = "เวียงเชียงรุ้ง"},
  new CityData { state = "เชียงราย", city = "เวียงป่าเป้า"},
  new CityData { state = "เชียงใหม่", city = "กัลยาณิวัฒนา"},
  new CityData { state = "เชียงใหม่", city = "จอมทอง"},
  new CityData { state = "เชียงใหม่", city = "เชียงดาว"},
  new CityData { state = "เชียงใหม่", city = "ไชยปราการ"},
  new CityData { state = "เชียงใหม่", city = "ดอยเต่า"},
  new CityData { state = "เชียงใหม่", city = "ดอยสะเก็ด"},
  new CityData { state = "เชียงใหม่", city = "ดอยหล่อ"},
  new CityData { state = "เชียงใหม่", city = "ฝาง"},
  new CityData { state = "เชียงใหม่", city = "พร้าว"},
  new CityData { state = "เชียงใหม่", city = "เมืองเชียงใหม่"},
  new CityData { state = "เชียงใหม่", city = "แม่แจ่ม"},
  new CityData { state = "เชียงใหม่", city = "แม่แตง"},
  new CityData { state = "เชียงใหม่", city = "แม่ริม"},
  new CityData { state = "เชียงใหม่", city = "แม่วาง"},
  new CityData { state = "เชียงใหม่", city = "แม่ออน"},
  new CityData { state = "เชียงใหม่", city = "แม่อาย"},
  new CityData { state = "เชียงใหม่", city = "เวียงแหง"},
  new CityData { state = "เชียงใหม่", city = "สะเมิง"},
  new CityData { state = "เชียงใหม่", city = "สันกำแพง"},
  new CityData { state = "เชียงใหม่", city = "สันทราย"},
  new CityData { state = "เชียงใหม่", city = "สันป่าตอง"},
  new CityData { state = "เชียงใหม่", city = "สารภี"},
  new CityData { state = "เชียงใหม่", city = "หางดง"},
  new CityData { state = "เชียงใหม่", city = "อมก๋อย"},
  new CityData { state = "เชียงใหม่", city = "ฮอด"},
  new CityData { state = "ตรัง", city = "กันตัง"},
  new CityData { state = "ตรัง", city = "นาโยง"},
  new CityData { state = "ตรัง", city = "ปะเหลียน"},
  new CityData { state = "ตรัง", city = "เมืองตรัง"},
  new CityData { state = "ตรัง", city = "ย่านตาขาว"},
  new CityData { state = "ตรัง", city = "รัษฎา"},
  new CityData { state = "ตรัง", city = "วังวิเศษ"},
  new CityData { state = "ตรัง", city = "สิเกา"},
  new CityData { state = "ตรัง", city = "ห้วยยอด"},
  new CityData { state = "ตรัง", city = "หาดสำราญ"},
  new CityData { state = "ตราด", city = "เกาะกูด"},
  new CityData { state = "ตราด", city = "เกาะช้าง"},
  new CityData { state = "ตราด", city = "เขาสมิง"},
  new CityData { state = "ตราด", city = "คลองใหญ่"},
  new CityData { state = "ตราด", city = "บ่อไร่"},
  new CityData { state = "ตราด", city = "เมืองตราด"},
  new CityData { state = "ตราด", city = "แหลมงอบ"},
  new CityData { state = "ตาก", city = "ท่าสองยาง"},
  new CityData { state = "ตาก", city = "บ้านตาก"},
  new CityData { state = "ตาก", city = "พบพระ"},
  new CityData { state = "ตาก", city = "เมืองตาก"},
  new CityData { state = "ตาก", city = "แม่ระมาด"},
  new CityData { state = "ตาก", city = "แม่สอด"},
  new CityData { state = "ตาก", city = "วังเจ้า"},
  new CityData { state = "ตาก", city = "สามเงา"},
  new CityData { state = "ตาก", city = "อุ้มผาง"},
  new CityData { state = "นครนายก", city = "บ้านนา"},
  new CityData { state = "นครนายก", city = "ปากพลี"},
  new CityData { state = "นครนายก", city = "เมืองนครนายก"},
  new CityData { state = "นครนายก", city = "องครักษ์"},
  new CityData { state = "นครปฐม", city = "กำแพงแสน"},
  new CityData { state = "นครปฐม", city = "ดอนตูม"},
  new CityData { state = "นครปฐม", city = "นครชัยศรี"},
  new CityData { state = "นครปฐม", city = "บางเลน"},
  new CityData { state = "นครปฐม", city = "พุทธมณฑล"},
  new CityData { state = "นครปฐม", city = "เมืองนครปฐม"},
  new CityData { state = "นครปฐม", city = "สามพราน"},
  new CityData { state = "นครพนม", city = "ท่าอุเทน"},
  new CityData { state = "นครพนม", city = "ธาตุพนม"},
  new CityData { state = "นครพนม", city = "นาแก"},
  new CityData { state = "นครพนม", city = "นาทม"},
  new CityData { state = "นครพนม", city = "นาหว้า"},
  new CityData { state = "นครพนม", city = "บ้านแพง"},
  new CityData { state = "นครพนม", city = "ปลาปาก"},
  new CityData { state = "นครพนม", city = "โพนสวรรค์"},
  new CityData { state = "นครพนม", city = "เมืองนครพนม"},
  new CityData { state = "นครพนม", city = "เรณูนคร"},
  new CityData { state = "นครพนม", city = "วังยาง"},
  new CityData { state = "นครพนม", city = "ศรีสงคราม"},
  new CityData { state = "นครราชสีมา", city = "แก้งสนามนาง"},
  new CityData { state = "นครราชสีมา", city = "ขามทะเลสอ"},
  new CityData { state = "นครราชสีมา", city = "ขามสะแกแสง"},
  new CityData { state = "นครราชสีมา", city = "คง"},
  new CityData { state = "นครราชสีมา", city = "ครบุรี"},
  new CityData { state = "นครราชสีมา", city = "จักราช"},
  new CityData { state = "นครราชสีมา", city = "เฉลิมพระเกียรติ"},
  new CityData { state = "นครราชสีมา", city = "ชุมพวง"},
  new CityData { state = "นครราชสีมา", city = "โชคชัย"},
  new CityData { state = "นครราชสีมา", city = "ด่านขุนทด"},
  new CityData { state = "นครราชสีมา", city = "เทพารักษ์"},
  new CityData { state = "นครราชสีมา", city = "โนนแดง"},
  new CityData { state = "นครราชสีมา", city = "โนนไทย"},
  new CityData { state = "นครราชสีมา", city = "โนนสูง"},
  new CityData { state = "นครราชสีมา", city = "บัวลาย"},
  new CityData { state = "นครราชสีมา", city = "บัวใหญ่"},
  new CityData { state = "นครราชสีมา", city = "บ้านเหลื่อม"},
  new CityData { state = "นครราชสีมา", city = "ประทาย"},
  new CityData { state = "นครราชสีมา", city = "ปักธงชัย"},
  new CityData { state = "นครราชสีมา", city = "ปากช่อง"},
  new CityData { state = "นครราชสีมา", city = "พระทองคำ"},
  new CityData { state = "นครราชสีมา", city = "พิมาย"},
  new CityData { state = "นครราชสีมา", city = "เมืองนครราชสีมา"},
  new CityData { state = "นครราชสีมา", city = "เมืองยาง"},
  new CityData { state = "นครราชสีมา", city = "ลำทะเมนชัย"},
  new CityData { state = "นครราชสีมา", city = "วังน้ำเขียว"},
  new CityData { state = "นครราชสีมา", city = "สีคิ้ว"},
  new CityData { state = "นครราชสีมา", city = "สีดา"},
  new CityData { state = "นครราชสีมา", city = "สูงเนิน"},
  new CityData { state = "นครราชสีมา", city = "เสิงสาง"},
  new CityData { state = "นครราชสีมา", city = "หนองบุญมาก"},
  new CityData { state = "นครราชสีมา", city = "ห้วยแถลง"},
  new CityData { state = "นครศรีธรรมราช", city = "ขนอม"},
  new CityData { state = "นครศรีธรรมราช", city = "จุฬาภรณ์"},
  new CityData { state = "นครศรีธรรมราช", city = "ฉวาง"},
  new CityData { state = "นครศรีธรรมราช", city = "เฉลิมพระเกียรติ"},
  new CityData { state = "นครศรีธรรมราช", city = "ชะอวด"},
  new CityData { state = "นครศรีธรรมราช", city = "ช้างกลาง"},
  new CityData { state = "นครศรีธรรมราช", city = "เชียรใหญ่"},
  new CityData { state = "นครศรีธรรมราช", city = "ถ้ำพรรณรา"},
  new CityData { state = "นครศรีธรรมราช", city = "ท่าศาลา"},
  new CityData { state = "นครศรีธรรมราช", city = "ทุ่งสง"},
  new CityData { state = "นครศรีธรรมราช", city = "ทุ่งใหญ่"},
  new CityData { state = "นครศรีธรรมราช", city = "นบพิตำ"},
  new CityData { state = "นครศรีธรรมราช", city = "นาบอน"},
  new CityData { state = "นครศรีธรรมราช", city = "บางขัน"},
  new CityData { state = "นครศรีธรรมราช", city = "ปากพนัง"},
  new CityData { state = "นครศรีธรรมราช", city = "พรหมคีรี"},
  new CityData { state = "นครศรีธรรมราช", city = "พระพรหม"},
  new CityData { state = "นครศรีธรรมราช", city = "พิปูน"},
  new CityData { state = "นครศรีธรรมราช", city = "เมืองนครศรีธรรมราช"},
  new CityData { state = "นครศรีธรรมราช", city = "ร่อนพิบูลย์"},
  new CityData { state = "นครศรีธรรมราช", city = "ลานสกา"},
  new CityData { state = "นครศรีธรรมราช", city = "สิชล"},
  new CityData { state = "นครศรีธรรมราช", city = "หัวไทร"},
  new CityData { state = "นครสวรรค์", city = "เก้าเลี้ยว"},
  new CityData { state = "นครสวรรค์", city = "โกรกพระ"},
  new CityData { state = "นครสวรรค์", city = "ชุมตาบง"},
  new CityData { state = "นครสวรรค์", city = "ชุมแสง"},
  new CityData { state = "นครสวรรค์", city = "ตากฟ้า"},
  new CityData { state = "นครสวรรค์", city = "ตาคลี"},
  new CityData { state = "นครสวรรค์", city = "ท่าตะโก"},
  new CityData { state = "นครสวรรค์", city = "บรรพตพิสัย"},
  new CityData { state = "นครสวรรค์", city = "พยุหะคีรี"},
  new CityData { state = "นครสวรรค์", city = "ไพศาลี"},
  new CityData { state = "นครสวรรค์", city = "เมืองนครสวรรค์"},
  new CityData { state = "นครสวรรค์", city = "แม่เปิน"},
  new CityData { state = "นครสวรรค์", city = "แม่วงก์"},
  new CityData { state = "นครสวรรค์", city = "ลาดยาว"},
  new CityData { state = "นครสวรรค์", city = "หนองบัว"},
  new CityData { state = "นนทบุรี", city = "ไทรน้อย"},
  new CityData { state = "นนทบุรี", city = "บางกรวย"},
  new CityData { state = "นนทบุรี", city = "บางบัวทอง"},
  new CityData { state = "นนทบุรี", city = "บางใหญ่"},
  new CityData { state = "นนทบุรี", city = "ปากเกร็ด"},
  new CityData { state = "นนทบุรี", city = "เมืองนนทบุรี"},
  new CityData { state = "นราธิวาส", city = "จะแนะ"},
  new CityData { state = "นราธิวาส", city = "เจาะไอร้อง"},
  new CityData { state = "นราธิวาส", city = "ตากใบ"},
  new CityData { state = "นราธิวาส", city = "บาเจาะ"},
  new CityData { state = "นราธิวาส", city = "เมืองนราธิวาส"},
  new CityData { state = "นราธิวาส", city = "ยี่งอ"},
  new CityData { state = "นราธิวาส", city = "ระแงะ"},
  new CityData { state = "นราธิวาส", city = "รือเสาะ"},
  new CityData { state = "นราธิวาส", city = "แว้ง"},
  new CityData { state = "นราธิวาส", city = "ศรีสาคร"},
  new CityData { state = "นราธิวาส", city = "สุคิริน"},
  new CityData { state = "นราธิวาส", city = "สุไหงโกลก"},
  new CityData { state = "นราธิวาส", city = "สุไหงปาดี"},
  new CityData { state = "น่าน", city = "เฉลิมพระเกียรติ"},
  new CityData { state = "น่าน", city = "เชียงกลาง"},
  new CityData { state = "น่าน", city = "ท่าวังผา"},
  new CityData { state = "น่าน", city = "ทุ่งช้าง"},
  new CityData { state = "น่าน", city = "นาน้อย"},
  new CityData { state = "น่าน", city = "นาหมื่น"},
  new CityData { state = "น่าน", city = "บ่อเกลือ"},
  new CityData { state = "น่าน", city = "บ้านหลวง"},
  new CityData { state = "น่าน", city = "ปัว"},
  new CityData { state = "น่าน", city = "ภูเพียง"},
  new CityData { state = "น่าน", city = "เมืองน่าน"},
  new CityData { state = "น่าน", city = "แม่จริม"},
  new CityData { state = "น่าน", city = "เวียงสา"},
  new CityData { state = "น่าน", city = "สองแคว"},
  new CityData { state = "น่าน", city = "สันติสุข"},
  new CityData { state = "บึงกาฬ", city = "เซกา"},
  new CityData { state = "บึงกาฬ", city = "โซ่พิสัย"},
  new CityData { state = "บึงกาฬ", city = "บึงโขงหลง"},
  new CityData { state = "บึงกาฬ", city = "บุ่งคล้า"},
  new CityData { state = "บึงกาฬ", city = "ปากคาด"},
  new CityData { state = "บึงกาฬ", city = "พรเจริญ"},
  new CityData { state = "บึงกาฬ", city = "เมืองบึงกาฬ"},
  new CityData { state = "บึงกาฬ", city = "ศรีวิไล"},
  new CityData { state = "บุรีรัมย์", city = "กระสัง"},
  new CityData { state = "บุรีรัมย์", city = "คูเมือง"},
  new CityData { state = "บุรีรัมย์", city = "แคนดง"},
  new CityData { state = "บุรีรัมย์", city = "เฉลิมพระเกียรติ"},
  new CityData { state = "บุรีรัมย์", city = "ชำนิ"},
  new CityData { state = "บุรีรัมย์", city = "นางรอง"},
  new CityData { state = "บุรีรัมย์", city = "นาโพธิ์"},
  new CityData { state = "บุรีรัมย์", city = "โนนดินแดง"},
  new CityData { state = "บุรีรัมย์", city = "โนนสุวรรณ"},
  new CityData { state = "บุรีรัมย์", city = "บ้านกรวด"},
  new CityData { state = "บุรีรัมย์", city = "บ้านด่าน"},
  new CityData { state = "บุรีรัมย์", city = "บ้านใหม่ไชยพจน์"},
  new CityData { state = "บุรีรัมย์", city = "ประโคนชัย"},
  new CityData { state = "บุรีรัมย์", city = "ปะคำ"},
  new CityData { state = "บุรีรัมย์", city = "พลับพลาชัย"},
  new CityData { state = "บุรีรัมย์", city = "พุทไธสง"},
  new CityData { state = "บุรีรัมย์", city = "เมืองบุรีรัมย์"},
  new CityData { state = "บุรีรัมย์", city = "ละหานทราย"},
  new CityData { state = "บุรีรัมย์", city = "ลำปลายมาศ"},
  new CityData { state = "บุรีรัมย์", city = "สตึก"},
  new CityData { state = "บุรีรัมย์", city = "หนองกี่"},
  new CityData { state = "บุรีรัมย์", city = "หนองหงส์"},
  new CityData { state = "บุรีรัมย์", city = "ห้วยราช"},
  new CityData { state = "ปทุมธานี", city = "คลองหลวง"},
  new CityData { state = "ปทุมธานี", city = "ธัญบุรี"},
  new CityData { state = "ปทุมธานี", city = "เมืองปทุมธานี"},
  new CityData { state = "ปทุมธานี", city = "ลาดหลุมแก้ว"},
  new CityData { state = "ปทุมธานี", city = "ลำลูกกา"},
  new CityData { state = "ปทุมธานี", city = "สามโคก"},
  new CityData { state = "ปทุมธานี", city = "หนองเสือ"},
  new CityData { state = "ประจวบคีรีขันธ์", city = "กุยบุรี"},
  new CityData { state = "ประจวบคีรีขันธ์", city = "ทับสะแก"},
  new CityData { state = "ประจวบคีรีขันธ์", city = "บางสะพาน"},
  new CityData { state = "ประจวบคีรีขันธ์", city = "บางสะพานน้อย"},
  new CityData { state = "ประจวบคีรีขันธ์", city = "ปราณบุรี"},
  new CityData { state = "ประจวบคีรีขันธ์", city = "เมืองประจวบคีรีขันธ์"},
  new CityData { state = "ประจวบคีรีขันธ์", city = "สามร้อยยอด"},
  new CityData { state = "ประจวบคีรีขันธ์", city = "หัวหิน"},
  new CityData { state = "ปราจีนบุรี", city = "กบินทร์บุรี"},
  new CityData { state = "ปราจีนบุรี", city = "นาดี"},
  new CityData { state = "ปราจีนบุรี", city = "บ้านสร้าง"},
  new CityData { state = "ปราจีนบุรี", city = "ประจันตคาม"},
  new CityData { state = "ปราจีนบุรี", city = "เมืองปราจีนบุรี"},
  new CityData { state = "ปราจีนบุรี", city = "ศรีมหาโพธิ"},
  new CityData { state = "ปราจีนบุรี", city = "ศรีมโหสถ"},
  new CityData { state = "ปัตตานี", city = "กะพ้อ"},
  new CityData { state = "ปัตตานี", city = "โคกโพธิ์"},
  new CityData { state = "ปัตตานี", city = "ทุ่งยางแดง"},
  new CityData { state = "ปัตตานี", city = "ปะนาเระ"},
  new CityData { state = "ปัตตานี", city = "มายอ"},
  new CityData { state = "ปัตตานี", city = "เมืองปัตตานี"},
  new CityData { state = "ปัตตานี", city = "แม่ลาน"},
  new CityData { state = "ปัตตานี", city = "ไม้แก่น"},
  new CityData { state = "ปัตตานี", city = "ยะรัง"},
  new CityData { state = "ปัตตานี", city = "ยะหริ่ง"},
  new CityData { state = "ปัตตานี", city = "สายบุรี"},
  new CityData { state = "ปัตตานี", city = "หนองจิก"},
  new CityData { state = "พระนครศรีอยุธยา", city = "ท่าเรือ"},
  new CityData { state = "พระนครศรีอยุธยา", city = "นครหลวง"},
  new CityData { state = "พระนครศรีอยุธยา", city = "บางซ้าย"},
  new CityData { state = "พระนครศรีอยุธยา", city = "บางไทร"},
  new CityData { state = "พระนครศรีอยุธยา", city = "บางบาล"},
  new CityData { state = "พระนครศรีอยุธยา", city = "บางปะหัน"},
  new CityData { state = "พระนครศรีอยุธยา", city = "บางปะอิน"},
  new CityData { state = "พระนครศรีอยุธยา", city = "บ้านแพรก"},
  new CityData { state = "พระนครศรีอยุธยา", city = "ผักไห่"},
  new CityData { state = "พระนครศรีอยุธยา", city = "พระนครศรีอยุธยา"},
  new CityData { state = "พระนครศรีอยุธยา", city = "ภาชี"},
  new CityData { state = "พระนครศรีอยุธยา", city = "มหาราช"},
  new CityData { state = "พระนครศรีอยุธยา", city = "ลาดบัวหลวง"},
  new CityData { state = "พระนครศรีอยุธยา", city = "วังน้อย"},
  new CityData { state = "พระนครศรีอยุธยา", city = "เสนา"},
  new CityData { state = "พระนครศรีอยุธยา", city = "อุทัย"},
  new CityData { state = "พะเยา", city = "จุน"},
  new CityData { state = "พะเยา", city = "เชียงคำ"},
  new CityData { state = "พะเยา", city = "เชียงม่วน"},
  new CityData { state = "พะเยา", city = "ดอกคำใต้"},
  new CityData { state = "พะเยา", city = "ปง"},
  new CityData { state = "พะเยา", city = "ภูกามยาว"},
  new CityData { state = "พะเยา", city = "ภูซาง"},
  new CityData { state = "พะเยา", city = "เมืองพะเยา"},
  new CityData { state = "พะเยา", city = "แม่ใจ"},
  new CityData { state = "พังงา", city = "กะปง"},
  new CityData { state = "พังงา", city = "เกาะยาว"},
  new CityData { state = "พังงา", city = "คุระบุรี"},
  new CityData { state = "พังงา", city = "ตะกั่วทุ่ง"},
  new CityData { state = "พังงา", city = "ตะกั่วป่า"},
  new CityData { state = "พังงา", city = "ทับปุด"},
  new CityData { state = "พังงา", city = "ท้ายเหมือง"},
  new CityData { state = "พังงา", city = "เมืองพังงา"},
  new CityData { state = "พัทลุง", city = "กงหรา"},
  new CityData { state = "พัทลุง", city = "เขาชัยสน"},
  new CityData { state = "พัทลุง", city = "ควนขนุน"},
  new CityData { state = "พัทลุง", city = "ตะโหมด"},
  new CityData { state = "พัทลุง", city = "บางแก้ว"},
  new CityData { state = "พัทลุง", city = "ปากพะยูน"},
  new CityData { state = "พัทลุง", city = "ป่าบอน"},
  new CityData { state = "พัทลุง", city = "ป่าพะยอม"},
  new CityData { state = "พัทลุง", city = "เมืองพัทลุง"},
  new CityData { state = "พัทลุง", city = "ศรีนครินทร์"},
  new CityData { state = "พัทลุง", city = "ศรีบรรพต"},
  new CityData { state = "พิจิตร", city = "ดงเจริญ"},
  new CityData { state = "พิจิตร", city = "ตะพานหิน"},
  new CityData { state = "พิจิตร", city = "ทับคล้อ"},
  new CityData { state = "พิจิตร", city = "บางมูลนาก"},
  new CityData { state = "พิจิตร", city = "บึงนาราง"},
  new CityData { state = "พิจิตร", city = "โพทะเล"},
  new CityData { state = "พิจิตร", city = "โพธิ์ประทับช้าง"},
  new CityData { state = "พิจิตร", city = "เมืองพิจิตร"},
  new CityData { state = "พิจิตร", city = "วชิรบารมี"},
  new CityData { state = "พิจิตร", city = "วังทรายพูน"},
  new CityData { state = "พิจิตร", city = "สากเหล็ก"},
  new CityData { state = "พิจิตร", city = "สามง่าม"},
  new CityData { state = "พิษณุโลก", city = "ชาติตระการ"},
  new CityData { state = "พิษณุโลก", city = "นครไทย"},
  new CityData { state = "พิษณุโลก", city = "เนินมะปราง"},
  new CityData { state = "พิษณุโลก", city = "บางกระทุ่ม"},
  new CityData { state = "พิษณุโลก", city = "บางระกำ"},
  new CityData { state = "พิษณุโลก", city = "พรหมพิราม"},
  new CityData { state = "พิษณุโลก", city = "เมืองพิษณุโลก"},
  new CityData { state = "พิษณุโลก", city = "วังทอง"},
  new CityData { state = "พิษณุโลก", city = "วัดโบสถ์"},
  new CityData { state = "เพชรบุรี", city = "แก่งกระจาน"},
  new CityData { state = "เพชรบุรี", city = "เขาย้อย"},
  new CityData { state = "เพชรบุรี", city = "ชะอำ"},
  new CityData { state = "เพชรบุรี", city = "ท่ายาง"},
  new CityData { state = "เพชรบุรี", city = "บ้านลาด"},
  new CityData { state = "เพชรบุรี", city = "บ้านแหลม"},
  new CityData { state = "เพชรบุรี", city = "เมืองเพชรบุรี"},
  new CityData { state = "เพชรบุรี", city = "หนองหญ้าปล้อง"},
  new CityData { state = "เพชรบูรณ์", city = "เขาค้อ"},
  new CityData { state = "เพชรบูรณ์", city = "ชนแดน"},
  new CityData { state = "เพชรบูรณ์", city = "น้ำหนาว"},
  new CityData { state = "เพชรบูรณ์", city = "บึงสามพัน"},
  new CityData { state = "เพชรบูรณ์", city = "เมืองเพชรบูรณ์"},
  new CityData { state = "เพชรบูรณ์", city = "วังโป่ง"},
  new CityData { state = "เพชรบูรณ์", city = "วิเชียรบุรี"},
  new CityData { state = "เพชรบูรณ์", city = "ศรีเทพ"},
  new CityData { state = "เพชรบูรณ์", city = "หนองไผ่"},
  new CityData { state = "เพชรบูรณ์", city = "หล่มเก่า"},
  new CityData { state = "เพชรบูรณ์", city = "หล่มสัก"},
  new CityData { state = "แพร่", city = "เด่นชัย"},
  new CityData { state = "แพร่", city = "เมืองแพร่"},
  new CityData { state = "แพร่", city = "ร้องกวาง"},
  new CityData { state = "แพร่", city = "ลอง"},
  new CityData { state = "แพร่", city = "วังชิ้น"},
  new CityData { state = "แพร่", city = "สอง"},
  new CityData { state = "แพร่", city = "สูงเม่น"},
  new CityData { state = "แพร่", city = "หนองม่วงไข่"},
  new CityData { state = "ภูเก็ต", city = "กะทู้"},
  new CityData { state = "ภูเก็ต", city = "ถลาง"},
  new CityData { state = "ภูเก็ต", city = "เมืองภูเก็ต"},
  new CityData { state = "มหาสารคาม", city = "กันทรวิชัย"},
  new CityData { state = "มหาสารคาม", city = "กุดรัง"},
  new CityData { state = "มหาสารคาม", city = "แกดำ"},
  new CityData { state = "มหาสารคาม", city = "โกสุมพิสัย"},
  new CityData { state = "มหาสารคาม", city = "ชื่นชม"},
  new CityData { state = "มหาสารคาม", city = "เชียงยืน"},
  new CityData { state = "มหาสารคาม", city = "นาเชือก"},
  new CityData { state = "มหาสารคาม", city = "นาดูน"},
  new CityData { state = "มหาสารคาม", city = "บรบือ"},
  new CityData { state = "มหาสารคาม", city = "พยัคฆภูมิพิสัย"},
  new CityData { state = "มหาสารคาม", city = "เมืองมหาสารคาม"},
  new CityData { state = "มหาสารคาม", city = "ยางสีสุราช"},
  new CityData { state = "มหาสารคาม", city = "วาปีปทุม"},
  new CityData { state = "มุกดาหาร", city = "คำชะอี"},
  new CityData { state = "มุกดาหาร", city = "ดงหลวง"},
  new CityData { state = "มุกดาหาร", city = "ดอนตาล"},
  new CityData { state = "มุกดาหาร", city = "นิคมคำสร้อย"},
  new CityData { state = "มุกดาหาร", city = "เมืองมุกดาหาร"},
  new CityData { state = "มุกดาหาร", city = "หนองสูง"},
  new CityData { state = "มุกดาหาร", city = "หว้านใหญ่"},
  new CityData { state = "แม่ฮ่องสอน", city = "ขุนยวม"},
  new CityData { state = "แม่ฮ่องสอน", city = "ปางมะผ้า"},
  new CityData { state = "แม่ฮ่องสอน", city = "ปาย"},
  new CityData { state = "แม่ฮ่องสอน", city = "เมืองแม่ฮ่องสอน"},
  new CityData { state = "แม่ฮ่องสอน", city = "แม่ลาน้อย"},
  new CityData { state = "แม่ฮ่องสอน", city = "แม่สะเรียง"},
  new CityData { state = "แม่ฮ่องสอน", city = "สบเมย"},
  new CityData { state = "ยโสธร", city = "กุดชุม"},
  new CityData { state = "ยโสธร", city = "ค้อวัง"},
  new CityData { state = "ยโสธร", city = "คำเขื่อนแก้ว"},
  new CityData { state = "ยโสธร", city = "ทรายมูล"},
  new CityData { state = "ยโสธร", city = "ไทยเจริญ"},
  new CityData { state = "ยโสธร", city = "ป่าติ้ว"},
  new CityData { state = "ยโสธร", city = "มหาชนะชัย"},
  new CityData { state = "ยโสธร", city = "เมืองยโสธร"},
  new CityData { state = "ยโสธร", city = "เลิงนกทา"},
  new CityData { state = "ยะลา", city = "กรงปินัง"},
  new CityData { state = "ยะลา", city = "กาบัง"},
  new CityData { state = "ยะลา", city = "ธารโต"},
  new CityData { state = "ยะลา", city = "บันนังสตา"},
  new CityData { state = "ยะลา", city = "เบตง"},
  new CityData { state = "ยะลา", city = "เมืองยะลา"},
  new CityData { state = "ยะลา", city = "ยะหา"},
  new CityData { state = "ยะลา", city = "รามัน"},
  new CityData { state = "ร้อยเอ็ด", city = "เกษตรวิสัย"},
  new CityData { state = "ร้อยเอ็ด", city = "จตุรพักตรพิมาน"},
  new CityData { state = "ร้อยเอ็ด", city = "จังหาร"},
  new CityData { state = "ร้อยเอ็ด", city = "เชียงขวัญ"},
  new CityData { state = "ร้อยเอ็ด", city = "ทุ่งเขาหลวง"},
  new CityData { state = "ร้อยเอ็ด", city = "ธวัชบุรี"},
  new CityData { state = "ร้อยเอ็ด", city = "ปทุมรัตต์"},
  new CityData { state = "ร้อยเอ็ด", city = "พนมไพร"},
  new CityData { state = "ร้อยเอ็ด", city = "โพธิ์ชัย"},
  new CityData { state = "ร้อยเอ็ด", city = "โพนทราย"},
  new CityData { state = "ร้อยเอ็ด", city = "โพนทอง"},
  new CityData { state = "ร้อยเอ็ด", city = "เมยวดี"},
  new CityData { state = "ร้อยเอ็ด", city = "เมืองร้อยเอ็ด"},
  new CityData { state = "ร้อยเอ็ด", city = "เมืองสรวง"},
  new CityData { state = "ร้อยเอ็ด", city = "ศรีสมเด็จ"},
  new CityData { state = "ร้อยเอ็ด", city = "สุวรรณภูมิ"},
  new CityData { state = "ร้อยเอ็ด", city = "เสลภูมิ"},
  new CityData { state = "ร้อยเอ็ด", city = "หนองพอก"},
  new CityData { state = "ร้อยเอ็ด", city = "หนองฮี"},
  new CityData { state = "ร้อยเอ็ด", city = "อาจสามารถ"},
  new CityData { state = "ระนอง", city = "กระบุรี"},
  new CityData { state = "ระนอง", city = "กะเปอร์"},
  new CityData { state = "ระนอง", city = "เมืองระนอง"},
  new CityData { state = "ระนอง", city = "ละอุ่น"},
  new CityData { state = "ระนอง", city = "สุขสำราญ"},
  new CityData { state = "ระยอง", city = "แกลง"},
  new CityData { state = "ระยอง", city = "เขาชะเมา"},
  new CityData { state = "ระยอง", city = "นิคมพัฒนา"},
  new CityData { state = "ระยอง", city = "บ้านค่าย"},
  new CityData { state = "ระยอง", city = "บ้านฉาง"},
  new CityData { state = "ระยอง", city = "ปลวกแดง"},
  new CityData { state = "ระยอง", city = "เมืองระยอง"},
  new CityData { state = "ระยอง", city = "วังจันทร์"},
  new CityData { state = "ราชบุรี", city = "จอมบึง"},
  new CityData { state = "ราชบุรี", city = "ดำเนินสะดวก"},
  new CityData { state = "ราชบุรี", city = "บางแพ"},
  new CityData { state = "ราชบุรี", city = "บ้านคา"},
  new CityData { state = "ราชบุรี", city = "บ้านโป่ง"},
  new CityData { state = "ราชบุรี", city = "ปากท่อ"},
  new CityData { state = "ราชบุรี", city = "โพธาราม"},
  new CityData { state = "ราชบุรี", city = "เมืองราชบุรี"},
  new CityData { state = "ราชบุรี", city = "วัดเพลง"},
  new CityData { state = "ราชบุรี", city = "สวนผึ้ง"},
  new CityData { state = "ลพบุรี", city = "โคกเจริญ"},
  new CityData { state = "ลพบุรี", city = "โคกสำโรง"},
  new CityData { state = "ลพบุรี", city = "ชัยบาดาล"},
  new CityData { state = "ลพบุรี", city = "ท่าวุ้ง"},
  new CityData { state = "ลพบุรี", city = "ท่าหลวง"},
  new CityData { state = "ลพบุรี", city = "บ้านหมี่"},
  new CityData { state = "ลพบุรี", city = "พัฒนานิคม"},
  new CityData { state = "ลพบุรี", city = "เมืองลพบุรี"},
  new CityData { state = "ลพบุรี", city = "ลำสนธิ"},
  new CityData { state = "ลพบุรี", city = "สระโบสถ์"},
  new CityData { state = "ลพบุรี", city = "หนองม่วง"},
  new CityData { state = "ลำปาง", city = "เกาะคา"},
  new CityData { state = "ลำปาง", city = "งาว"},
  new CityData { state = "ลำปาง", city = "แจ้ห่ม"},
  new CityData { state = "ลำปาง", city = "เถิน"},
  new CityData { state = "ลำปาง", city = "เมืองปาน"},
  new CityData { state = "ลำปาง", city = "เมืองลำปาง"},
  new CityData { state = "ลำปาง", city = "แม่ทะ"},
  new CityData { state = "ลำปาง", city = "แม่พริก"},
  new CityData { state = "ลำปาง", city = "แม่เมาะ"},
  new CityData { state = "ลำปาง", city = "วังเหนือ"},
  new CityData { state = "ลำปาง", city = "สบปราบ"},
  new CityData { state = "ลำปาง", city = "เสริมงาม"},
  new CityData { state = "ลำปาง", city = "ห้างฉัตร"},
  new CityData { state = "ลำพูน", city = "ทุ่งหัวช้าง"},
  new CityData { state = "ลำพูน", city = "บ้านธิ"},
  new CityData { state = "ลำพูน", city = "บ้านโฮ่ง"},
  new CityData { state = "ลำพูน", city = "ป่าซาง"},
  new CityData { state = "ลำพูน", city = "เมืองลำพูน"},
  new CityData { state = "ลำพูน", city = "แม่ทา"},
  new CityData { state = "ลำพูน", city = "ลี้"},
  new CityData { state = "ลำพูน", city = "เวียงหนองล่อง"},
  new CityData { state = "เลย", city = "เชียงคาน"},
  new CityData { state = "เลย", city = "ด่านซ้าย"},
  new CityData { state = "เลย", city = "ท่าลี่"},
  new CityData { state = "เลย", city = "นาด้วง"},
  new CityData { state = "เลย", city = "นาแห้ว"},
  new CityData { state = "เลย", city = "ปากชม"},
  new CityData { state = "เลย", city = "ผาขาว"},
  new CityData { state = "เลย", city = "ภูกระดึง"},
  new CityData { state = "เลย", city = "ภูเรือ"},
  new CityData { state = "เลย", city = "ภูหลวง"},
  new CityData { state = "เลย", city = "เมืองเลย"},
  new CityData { state = "เลย", city = "วังสะพุง"},
  new CityData { state = "เลย", city = "หนองหิน"},
  new CityData { state = "เลย", city = "เอราวัณ"},
  new CityData { state = "ศรีสะเกษ", city = "กันทรลักษ์"},
  new CityData { state = "ศรีสะเกษ", city = "กันทรารมย์"},
  new CityData { state = "ศรีสะเกษ", city = "ขุขันธ์"},
  new CityData { state = "ศรีสะเกษ", city = "ขุนหาญ"},
  new CityData { state = "ศรีสะเกษ", city = "น้ำเกลี้ยง"},
  new CityData { state = "ศรีสะเกษ", city = "โนนคูณ"},
  new CityData { state = "ศรีสะเกษ", city = "บึงบูรพ์"},
  new CityData { state = "ศรีสะเกษ", city = "เบญจลักษ์"},
  new CityData { state = "ศรีสะเกษ", city = "ปรางค์กู่"},
  new CityData { state = "ศรีสะเกษ", city = "พยุห์"},
  new CityData { state = "ศรีสะเกษ", city = "โพธิ์ศรีสุวรรณ"},
  new CityData { state = "ศรีสะเกษ", city = "ไพรบึง"},
  new CityData { state = "ศรีสะเกษ", city = "ภูสิงห์"},
  new CityData { state = "ศรีสะเกษ", city = "เมืองจันทร์"},
  new CityData { state = "ศรีสะเกษ", city = "เมืองศรีสะเกษ"},
  new CityData { state = "ศรีสะเกษ", city = "ยางชุมน้อย"},
  new CityData { state = "ศรีสะเกษ", city = "ราษีไศล"},
  new CityData { state = "ศรีสะเกษ", city = "วังหิน"},
  new CityData { state = "ศรีสะเกษ", city = "ศรีรัตนะ"},
  new CityData { state = "ศรีสะเกษ", city = "ศิลาลาด"},
  new CityData { state = "ศรีสะเกษ", city = "ห้วยทับทัน"},
  new CityData { state = "ศรีสะเกษ", city = "อุทุมพรพิสัย"},
  new CityData { state = "สกลนคร", city = "กุดบาก"},
  new CityData { state = "สกลนคร", city = "กุสุมาลย์"},
  new CityData { state = "สกลนคร", city = "คำตากล้า"},
  new CityData { state = "สกลนคร", city = "โคกศรีสุพรรณ"},
  new CityData { state = "สกลนคร", city = "เจริญศิลป์"},
  new CityData { state = "สกลนคร", city = "เต่างอย"},
  new CityData { state = "สกลนคร", city = "นิคมน้ำอูน"},
  new CityData { state = "สกลนคร", city = "บ้านม่วง"},
  new CityData { state = "สกลนคร", city = "พรรณานิคม"},
  new CityData { state = "สกลนคร", city = "พังโคน"},
  new CityData { state = "สกลนคร", city = "โพนนาแก้ว"},
  new CityData { state = "สกลนคร", city = "ภูพาน"},
  new CityData { state = "สกลนคร", city = "เมืองสกลนคร"},
  new CityData { state = "สกลนคร", city = "วานรนิวาส"},
  new CityData { state = "สกลนคร", city = "วาริชภูมิ"},
  new CityData { state = "สกลนคร", city = "สว่างแดนดิน"},
  new CityData { state = "สกลนคร", city = "ส่องดาว"},
  new CityData { state = "สกลนคร", city = "อากาศอำนวย"},
  new CityData { state = "สงขลา", city = "กระแสสินธุ์"},
  new CityData { state = "สงขลา", city = "คลองหอยโข่ง"},
  new CityData { state = "สงขลา", city = "ควนเนียง"},
  new CityData { state = "สงขลา", city = "จะนะ"},
  new CityData { state = "สงขลา", city = "เทพา"},
  new CityData { state = "สงขลา", city = "นาทวี"},
  new CityData { state = "สงขลา", city = "นาหม่อม"},
  new CityData { state = "สงขลา", city = "บางกล่ำ"},
  new CityData { state = "สงขลา", city = "เมืองสงขลา"},
  new CityData { state = "สงขลา", city = "ระโนด"},
  new CityData { state = "สงขลา", city = "รัตภูมิ"},
  new CityData { state = "สงขลา", city = "สทิงพระ"},
  new CityData { state = "สงขลา", city = "สะเดา"},
  new CityData { state = "สงขลา", city = "สะบ้าย้อย"},
  new CityData { state = "สงขลา", city = "สิงหนคร"},
  new CityData { state = "สงขลา", city = "หาดใหญ่"},
  new CityData { state = "สตูล", city = "ควนกาหลง"},
  new CityData { state = "สตูล", city = "ควนโดน"},
  new CityData { state = "สตูล", city = "ท่าแพ"},
  new CityData { state = "สตูล", city = "ทุ่งหว้า"},
  new CityData { state = "สตูล", city = "มะนัง"},
  new CityData { state = "สตูล", city = "เมืองสตูล"},
  new CityData { state = "สตูล", city = "ละงู"},
  new CityData { state = "สมุทรปราการ", city = "บางบ่อ"},
  new CityData { state = "สมุทรปราการ", city = "บางพลี"},
  new CityData { state = "สมุทรปราการ", city = "บางเสาธง"},
  new CityData { state = "สมุทรปราการ", city = "พระประแดง"},
  new CityData { state = "สมุทรปราการ", city = "พระสมุทรเจดีย์"},
  new CityData { state = "สมุทรปราการ", city = "เมืองสมุทรปราการ"},
  new CityData { state = "สมุทรสงคราม", city = "บางคนที"},
  new CityData { state = "สมุทรสงคราม", city = "เมืองสมุทรสงคราม"},
  new CityData { state = "สมุทรสงคราม", city = "อัมพวา"},
  new CityData { state = "สมุทรสาคร", city = "กระทุ่มแบน"},
  new CityData { state = "สมุทรสาคร", city = "บ้านแพ้ว"},
  new CityData { state = "สมุทรสาคร", city = "เมืองสมุทรสาคร"},
  new CityData { state = "สระแก้ว", city = "เขาฉกรรจ์"},
  new CityData { state = "สระแก้ว", city = "คลองหาด"},
  new CityData { state = "สระแก้ว", city = "โคกสูง"},
  new CityData { state = "สระแก้ว", city = "ตาพระยา"},
  new CityData { state = "สระแก้ว", city = "เมืองสระแก้ว"},
  new CityData { state = "สระแก้ว", city = "วังน้ำเย็น"},
  new CityData { state = "สระแก้ว", city = "วังสมบูรณ์"},
  new CityData { state = "สระแก้ว", city = "วัฒนานคร"},
  new CityData { state = "สระแก้ว", city = "อรัญประเทศ"},
  new CityData { state = "สระบุรี", city = "แก่งคอย"},
  new CityData { state = "สระบุรี", city = "เฉลิมพระเกียรติ"},
  new CityData { state = "สระบุรี", city = "ดอนพุด"},
  new CityData { state = "สระบุรี", city = "บ้านหมอ"},
  new CityData { state = "สระบุรี", city = "พระพุทธบาท"},
  new CityData { state = "สระบุรี", city = "มวกเหล็ก"},
  new CityData { state = "สระบุรี", city = "เมืองสระบุรี"},
  new CityData { state = "สระบุรี", city = "วังม่วง"},
  new CityData { state = "สระบุรี", city = "วิหารแดง"},
  new CityData { state = "สระบุรี", city = "เสาไห้"},
  new CityData { state = "สระบุรี", city = "หนองแค"},
  new CityData { state = "สระบุรี", city = "หนองแซง"},
  new CityData { state = "สระบุรี", city = "หนองโดน"},
  new CityData { state = "สิงห์บุรี", city = "ค่ายบางระจัน"},
  new CityData { state = "สิงห์บุรี", city = "ท่าช้าง"},
  new CityData { state = "สิงห์บุรี", city = "บางระจัน"},
  new CityData { state = "สิงห์บุรี", city = "พรหมบุรี"},
  new CityData { state = "สิงห์บุรี", city = "เมืองสิงห์บุรี"},
  new CityData { state = "สิงห์บุรี", city = "อินทร์บุรี"},
  new CityData { state = "สุโขทัย", city = "กงไกรลาศ"},
  new CityData { state = "สุโขทัย", city = "คีรีมาศ"},
  new CityData { state = "สุโขทัย", city = "ทุ่งเสลี่ยม"},
  new CityData { state = "สุโขทัย", city = "บ้านด่านลานหอย"},
  new CityData { state = "สุโขทัย", city = "เมืองสุโขทัย"},
  new CityData { state = "สุโขทัย", city = "ศรีนคร"},
  new CityData { state = "สุโขทัย", city = "ศรีสัชนาลัย"},
  new CityData { state = "สุโขทัย", city = "ศรีสำโรง"},
  new CityData { state = "สุโขทัย", city = "สวรรคโลก"},
  new CityData { state = "สุพรรณบุรี", city = "ดอนเจดีย์"},
  new CityData { state = "สุพรรณบุรี", city = "ด่านช้าง"},
  new CityData { state = "สุพรรณบุรี", city = "เดิมบางนางบวช"},
  new CityData { state = "สุพรรณบุรี", city = "บางปลาม้า"},
  new CityData { state = "สุพรรณบุรี", city = "เมืองสุพรรณบุรี"},
  new CityData { state = "สุพรรณบุรี", city = "ศรีประจันต์"},
  new CityData { state = "สุพรรณบุรี", city = "สองพี่น้อง"},
  new CityData { state = "สุพรรณบุรี", city = "สามชุก"},
  new CityData { state = "สุพรรณบุรี", city = "หนองหญ้าไซ"},
  new CityData { state = "สุพรรณบุรี", city = "อู่ทอง"},
  new CityData { state = "สุราษฎร์ธานี", city = "กาญจนดิษฐ์"},
  new CityData { state = "สุราษฎร์ธานี", city = "เกาะพะงัน"},
  new CityData { state = "สุราษฎร์ธานี", city = "เกาะสมุย"},
  new CityData { state = "สุราษฎร์ธานี", city = "คีรีรัฐนิคม"},
  new CityData { state = "สุราษฎร์ธานี", city = "เคียนซา"},
  new CityData { state = "สุราษฎร์ธานี", city = "ชัยบุรี"},
  new CityData { state = "สุราษฎร์ธานี", city = "ไชยา"},
  new CityData { state = "สุราษฎร์ธานี", city = "ดอนสัก"},
  new CityData { state = "สุราษฎร์ธานี", city = "ท่าฉาง"},
  new CityData { state = "สุราษฎร์ธานี", city = "ท่าชนะ"},
  new CityData { state = "สุราษฎร์ธานี", city = "บ้านตาขุน"},
  new CityData { state = "สุราษฎร์ธานี", city = "บ้านนาเดิม"},
  new CityData { state = "สุราษฎร์ธานี", city = "บ้านนาสาร"},
  new CityData { state = "สุราษฎร์ธานี", city = "พนม"},
  new CityData { state = "สุราษฎร์ธานี", city = "พระแสง"},
  new CityData { state = "สุราษฎร์ธานี", city = "พุนพิน"},
  new CityData { state = "สุราษฎร์ธานี", city = "เมืองสุราษฎร์ธานี"},
  new CityData { state = "สุราษฎร์ธานี", city = "วิภาวดี"},
  new CityData { state = "สุราษฎร์ธานี", city = "เวียงสระ"},
  new CityData { state = "สุรินทร์", city = "กาบเชิง"},
  new CityData { state = "สุรินทร์", city = "เขวาสินรินทร์"},
  new CityData { state = "สุรินทร์", city = "จอมพระ"},
  new CityData { state = "สุรินทร์", city = "ชุมพลบุรี"},
  new CityData { state = "สุรินทร์", city = "ท่าตูม"},
  new CityData { state = "สุรินทร์", city = "โนนนารายณ์"},
  new CityData { state = "สุรินทร์", city = "บัวเชด"},
  new CityData { state = "สุรินทร์", city = "ปราสาท"},
  new CityData { state = "สุรินทร์", city = "พนมดงรัก"},
  new CityData { state = "สุรินทร์", city = "เมืองสุรินทร์"},
  new CityData { state = "สุรินทร์", city = "รัตนบุรี"},
  new CityData { state = "สุรินทร์", city = "ลำดวน"},
  new CityData { state = "สุรินทร์", city = "ศรีณรงค์"},
  new CityData { state = "สุรินทร์", city = "ศีขรภูมิ"},
  new CityData { state = "สุรินทร์", city = "สนม"},
  new CityData { state = "สุรินทร์", city = "สังขะ"},
  new CityData { state = "สุรินทร์", city = "สำโรงทาบ"},
  new CityData { state = "หนองคาย", city = "ท่าบ่อ"},
  new CityData { state = "หนองคาย", city = "เฝ้าไร่"},
  new CityData { state = "หนองคาย", city = "โพธิ์ตาก"},
  new CityData { state = "หนองคาย", city = "โพนพิสัย"},
  new CityData { state = "หนองคาย", city = "เมืองหนองคาย"},
  new CityData { state = "หนองคาย", city = "รัตนวาปี"},
  new CityData { state = "หนองคาย", city = "ศรีเชียงใหม่"},
  new CityData { state = "หนองคาย", city = "สระใคร"},
  new CityData { state = "หนองคาย", city = "สังคม"},
  new CityData { state = "หนองบัวลำภู", city = "นากลาง"},
  new CityData { state = "หนองบัวลำภู", city = "นาวัง"},
  new CityData { state = "หนองบัวลำภู", city = "โนนสัง"},
  new CityData { state = "หนองบัวลำภู", city = "เมืองหนองบัวลำภู"},
  new CityData { state = "หนองบัวลำภู", city = "ศรีบุญเรือง"},
  new CityData { state = "หนองบัวลำภู", city = "สุวรรณคูหา"},
  new CityData { state = "อ่างทอง", city = "ไชโย"},
  new CityData { state = "อ่างทอง", city = "ป่าโมก"},
  new CityData { state = "อ่างทอง", city = "โพธิ์ทอง"},
  new CityData { state = "อ่างทอง", city = "เมืองอ่างทอง"},
  new CityData { state = "อ่างทอง", city = "วิเศษชัยชาญ"},
  new CityData { state = "อ่างทอง", city = "สามโก้"},
  new CityData { state = "อ่างทอง", city = "แสวงหา"},
  new CityData { state = "อำนาจเจริญ", city = "ชานุมาน"},
  new CityData { state = "อำนาจเจริญ", city = "ปทุมราชวงศา"},
  new CityData { state = "อำนาจเจริญ", city = "พนา"},
  new CityData { state = "อำนาจเจริญ", city = "เมืองอำนาจเจริญ"},
  new CityData { state = "อำนาจเจริญ", city = "ลืออำนาจ"},
  new CityData { state = "อำนาจเจริญ", city = "เสนางคนิคม"},
  new CityData { state = "อำนาจเจริญ", city = "หัวตะพาน"},
  new CityData { state = "อุดรธานี", city = "กุดจับ"},
  new CityData { state = "อุดรธานี", city = "กุมภวาปี"},
  new CityData { state = "อุดรธานี", city = "กู่แก้ว"},
  new CityData { state = "อุดรธานี", city = "ไชยวาน"},
  new CityData { state = "อุดรธานี", city = "ทุ่งฝน"},
  new CityData { state = "อุดรธานี", city = "นายูง"},
  new CityData { state = "อุดรธานี", city = "น้ำโสม"},
  new CityData { state = "อุดรธานี", city = "โนนสะอาด"},
  new CityData { state = "อุดรธานี", city = "บ้านดุง"},
  new CityData { state = "อุดรธานี", city = "บ้านผือ"},
  new CityData { state = "อุดรธานี", city = "ประจักษ์ศิลปาคม"},
  new CityData { state = "อุดรธานี", city = "พิบูลย์รักษ์"},
  new CityData { state = "อุดรธานี", city = "เพ็ญ"},
  new CityData { state = "อุดรธานี", city = "เมืองอุดรธานี"},
  new CityData { state = "อุดรธานี", city = "วังสามหมอ"},
  new CityData { state = "อุดรธานี", city = "ศรีธาตุ"},
  new CityData { state = "อุดรธานี", city = "สร้างคอม"},
  new CityData { state = "อุดรธานี", city = "หนองวัวซอ"},
  new CityData { state = "อุดรธานี", city = "หนองแสง"},
  new CityData { state = "อุดรธานี", city = "หนองหาน"},
  new CityData { state = "อุตรดิตถ์", city = "ตรอน"},
  new CityData { state = "อุตรดิตถ์", city = "ทองแสนขัน"},
  new CityData { state = "อุตรดิตถ์", city = "ท่าปลา"},
  new CityData { state = "อุตรดิตถ์", city = "น้ำปาด"},
  new CityData { state = "อุตรดิตถ์", city = "บ้านโคก"},
  new CityData { state = "อุตรดิตถ์", city = "พิชัย"},
  new CityData { state = "อุตรดิตถ์", city = "ฟากท่า"},
  new CityData { state = "อุตรดิตถ์", city = "เมืองอุตรดิตถ์"},
  new CityData { state = "อุตรดิตถ์", city = "ลับแล"},
  new CityData { state = "อุทัยธานี", city = "ทัพทัน"},
  new CityData { state = "อุทัยธานี", city = "บ้านไร่"},
  new CityData { state = "อุทัยธานี", city = "เมืองอุทัยธานี"},
  new CityData { state = "อุทัยธานี", city = "ลานสัก"},
  new CityData { state = "อุทัยธานี", city = "สว่างอารมณ์"},
  new CityData { state = "อุทัยธานี", city = "หนองขาหย่าง"},
  new CityData { state = "อุทัยธานี", city = "หนองฉาง"},
  new CityData { state = "อุทัยธานี", city = "ห้วยคต"},
  new CityData { state = "อุบลราชธานี", city = "กุดข้าวปุ้น"},
  new CityData { state = "อุบลราชธานี", city = "เขมราฐ"},
  new CityData { state = "อุบลราชธานี", city = "เขื่องใน"},
  new CityData { state = "อุบลราชธานี", city = "โขงเจียม"},
  new CityData { state = "อุบลราชธานี", city = "ดอนมดแดง"},
  new CityData { state = "อุบลราชธานี", city = "เดชอุดม"},
  new CityData { state = "อุบลราชธานี", city = "ตระการพืชผล"},
  new CityData { state = "อุบลราชธานี", city = "ตาลสุม"},
  new CityData { state = "อุบลราชธานี", city = "ทุ่งศรีอุดม"},
  new CityData { state = "อุบลราชธานี", city = "นาจะหลวย"},
  new CityData { state = "อุบลราชธานี", city = "นาตาล"},
  new CityData { state = "อุบลราชธานี", city = "นาเยีย"},
  new CityData { state = "อุบลราชธานี", city = "น้ำขุ่น"},
  new CityData { state = "อุบลราชธานี", city = "น้ำยืน"},
  new CityData { state = "อุบลราชธานี", city = "บุณฑริก"},
  new CityData { state = "อุบลราชธานี", city = "พิบูลมังสาหาร"},
  new CityData { state = "อุบลราชธานี", city = "โพธิ์ไทร"},
  new CityData { state = "อุบลราชธานี", city = "ม่วงสามสิบ"},
  new CityData { state = "อุบลราชธานี", city = "เมืองอุบลราชธานี"},
  new CityData { state = "อุบลราชธานี", city = "วารินชำราบ"},
  new CityData { state = "อุบลราชธานี", city = "ศรีเมืองใหม่"},
  new CityData { state = "อุบลราชธานี", city = "สว่างวีระวงศ์"},
  new CityData { state = "อุบลราชธานี", city = "สำโรง"},
  new CityData { state = "อุบลราชธานี", city = "สิรินธร"},
  new CityData { state = "อุบลราชธานี", city = "เหล่าเสือโก้ก"}
            };


            return CityArrayData.Where(x => x.state == paramState) .Select(y => y.city) .ToArray(); ;
        }

        public static string[] getStates()
        {
            string[] itemsArray = {   "กรุงเทพมหานคร",
                "กระบี่",
                "กาญจนบุรี",
                "กาฬสินธุ์",
                "กำแพงเพชร",
                "ขอนแก่น",
                "จันทบุรี",
                "ฉะเชิงเทรา",
                "ชลบุรี",
                "ชัยนาท",
                "ชัยภูมิ",
                "ชุมพร",
                "เชียงราย",
                "เชียงใหม่",
                "ตรัง",
                "ตราด",
                "ตาก",
                "นครนายก",
                "นครปฐม",
                "นครพนม",
                "นครราชสีมา",
                "นครศรีธรรมราช",
                "นครสวรรค์",
                "นนทบุรี",
                "นราธิวาส",
                "น่าน",
                "บึงกาฬ",
                "บุรีรัมย์",
                "ปทุมธานี",
                "ประจวบคีรีขันธ์",
                "ปราจีนบุรี",
                "ปัตตานี",
                "พระนครศรีอยุธยา",
                "พะเยา",
                "พังงา",
                "พัทลุง",
                "พิจิตร",
                "พิษณุโลก",
                "เพชรบุรี",
                "เพชรบูรณ์",
                "แพร่",
                "ภูเก็ต",
                "มหาสารคาม",
                "มุกดาหาร",
                "แม่ฮ่องสอน",
                "ยโสธร",
                "ยะลา",
                "ร้อยเอ็ด",
                "ระนอง",
                "ระยอง",
                "ราชบุรี",
                "ลพบุรี",
                "ลำปาง",
                "ลำพูน",
                "เลย",
                "ศรีสะเกษ",
                "สกลนคร",
                "สงขลา",
                "สตูล",
                "สมุทรปราการ",
                "สมุทรสงคราม",
                "สมุทรสาคร",
                "สระแก้ว",
                "สระบุรี",
                "สิงห์บุรี",
                "สุโขทัย",
                "สุพรรณบุรี",
                "สุราษฎร์ธานี",
                "สุรินทร์",
                "หนองคาย",
                "หนองบัวลำภู",
                "อ่างทอง",
                "อำนาจเจริญ",
                "อุดรธานี",
                "อุตรดิตถ์",
                "อุทัยธานี",
                "อุบลราชธานี" };

            return itemsArray;
        }


        public static void generateErrorPage(string ErrorTitle, string ErrMessage = "", bool back = true, string ErrMoreDetail = "")
        {
            {
                var withBlock = HttpContext.Current;
                if (withBlock.CurrentHandler != null && withBlock.CurrentHandler is Page)
                {
                    Page p = (Page)withBlock.CurrentHandler;
                    ScriptManager s = ScriptManager.GetCurrent(p);
                    if (s != null && s.IsInAsyncPostBack)
                    {
                        throw new Exception(ErrorTitle + System.Environment.NewLine + System.Environment.NewLine + ErrMessage);
                        return;
                    }
                }

                withBlock.Response.Clear();
                withBlock.Response.Write("<!DOCTYPE html>");
                withBlock.Response.Write("<html>");
                withBlock.Response.Write("<head><title>Error</title>");
                withBlock.Response.Write("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
                withBlock.Response.Write("<meta http-equiv=\"Pragma\" content=\"no-cache\" />");
                withBlock.Response.Write("<meta http-equiv=\"expires\" content=\"0\" />");
                withBlock.Response.Write("<link href=\"" + withBlock.Request.ApplicationPath.TrimEnd('/') + "/CoreFunction/guStyle.css\" type=\"text/css\" rel=\"stylesheet\" />");
                withBlock.Response.Write("<style> * { box-sizing: border-box; } body { margin: 0; } </style>");
                withBlock.Response.Write("</head>");
                withBlock.Response.Write("<body>");
                withBlock.Response.Write("<div style=\"border: solid 1px #f00; overflow: auto; text-align: center; position: absolute; top: 5px; right: 5px; bottom: 5px; left: 5px; z-index: 1; font-size: 14px; line-height: 1.5em; padding: 14px;\">");
                withBlock.Response.Write("<table style=\"width: 100%; height: 100%;\"><tr><td style=\"vertical-align:middle;\">");
                withBlock.Response.Write("	<div class=\"ErrorTitle\">" + ErrorTitle + "</div>");
                withBlock.Response.Write("	<div style=\"margin: 12px 0;\">" + ErrMessage + "</div>");
                if (ErrMoreDetail != "")
                {
                    withBlock.Response.Write("<fieldset style=\"margin: 20px 0; text-align: left;\"><legend>Technical Information</legend>");
                    withBlock.Response.Write(ErrMoreDetail);
                    withBlock.Response.Write("</fieldset>");
                }
                if (back && withBlock.Request.UrlReferrer != null)
                    withBlock.Response.Write("		<input type=\"button\" class=\"bttn\" value=\"" + "ย้อนกลับ" + "\" onclick=\"history.back();\" />");
                withBlock.Response.Write("</td></tr></table></div>");
                withBlock.Response.Write("</body>");
                withBlock.Response.Write("</html>");
                withBlock.Response.End();
            }
        }

    }
}