using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using USILogicLayer.Protocol;
using Extensions.ByteArrayExtensions;
using DataAccessLayer;
using System.Threading;
using System.IO;
using Extensions.Loggers;
using USILogicLayer.Algorithms.Algorithm_Forms;
using System.Diagnostics;
using USILogicLayer.Algorithms;


namespace USILogicLayer.Algorithms
{
    public class Radar_Respect_Diag : Algorithm
    {
        public Radar_Respect_Diag(RayanDataModelDataSet.ECURow ECU, LogicManager logicManager)
                                                                            : base(ECU, logicManager)
        {
            Messages["ABSRespectDiag_AreYouSure", Language.English] = "Are you sure about performing the operation?";
            Messages["ABSRespectDiag_AreYouSure", Language.Farsi] = "آیا از انجام عملیات اطمینان دارید؟";
                         
            Messages["ABSRespectDiag_Canceled", Language.English] = "Operation canceled by user.";
            Messages["ABSRespectDiag_Canceled", Language.Farsi] = "عملیات توسط کاربر لغو شد";
                         
            Messages["ABSRespectDiag_Digit", Language.English] = " digit ";
            Messages["ABSRespectDiag_Digit", Language.Farsi] = " رقم ";
                         
            Messages["ABSRespectDiag_lessAllowedLimit", Language.English] = "The entered values are less than the allowed limit.";
            Messages["ABSRespectDiag_lessAllowedLimit", Language.Farsi] = "مقادیر وارد شده کمتر از حد مجاز است.";
                         
            Messages["ABSRespectDiag_EnterDate", Language.English] = " EnterDate ";
            Messages["ABSRespectDiag_EnterDate", Language.Farsi] = " تاریخ را وارد کنید ";
                         
            Messages["ABSRespectDiag_done", Language.English] = " Operation done ";
            Messages["ABSRespectDiag_done", Language.Farsi] = " مقادیر وارد شده به درستی ثبت شد ";

            Messages["ABSRespectDiag_Info", Language.English] = "Any action that causes a change in the crankshaft sensor position requires this operation.";
            Messages["ABSRespectDiag_Info", Language.Farsi] = "هر اقدامی که باعث تغییر موقعیت سنسور میل‌لنگ شود نیاز به انجام این عملیات دارد.";

            Messages["ABSRespectDiag_Warning", Language.English] = "This operation is required after: Camshaft sensor replacement, Engine speed sensor replacement, Crankshaft or camshaft replacement, Timing belt replacement, Timing adjustment, ECU replacement.";
            Messages["ABSRespectDiag_Warning", Language.Farsi] = "انجام این عملیات پس از موارد زیر الزامی است: تعویض سنسور میل بادامک، تعویض سنسور دور موتور، تعویض میل لنگ یا میل بادامک، تعویض تسمه تایم، تنظیم زمانبندی، تعویض ایسیو.";

            Messages["ABSRespectDiag_Condition", Language.English] = "VVT position setup conditions: First warm up the engine, turn the ignition off, then turn the ignition on.";
            Messages["ABSRespectDiag_Condition", Language.Farsi] = "شرایط تعریف موقعیت شیر VVT: ابتدا موتور را گرم کرده سوییچ را بسته و سپس سوییچ باز باشد.";

            Messages["ABSRespectDiag_Failed", Language.English] = "Operation failed";
            Messages["ABSRespectDiag_Failed", Language.Farsi] = "عملیات انجام نشد";

            Messages["ABSRespectDiag_Condition1", Language.English] = " After installing the new VGT,turn thr ignition ON.The engine must be stationary and vehicle speed must be 0.";
            Messages["ABSRespectDiag_Condition1", Language.Farsi] = " بعد از تعویض VGT موتوردر حالت ایستا باشد و سرعت صفر باشد .";

            Messages["ABSRespectDiag_Confirm", Language.English] = "Turn the ignition OFF for 5 seconds, then turn it ON. Press ENTER to continue.";
            Messages["ABSRespectDiag_Confirm", Language.Farsi] = "سوییچ را 5 ثانیه ببندید و سوییچ را باز کنید و برای ادامه اینتر را بزنید.";

            Messages["ABSRespectDiag_Success", Language.English] = "Definition completed successfully. Turn OFF the VGT, wait 1 minute, then turn it ON again.";
            Messages["ABSRespectDiag_Success", Language.Farsi] = "تعریف با موفقیت انجام شد. VGT را خاموش کرده و یک دقیقه صبر کنید و مجدد روشن کنید.";

            Messages["ABSRespectDiag_InputRange", Language.English] = "Enter a value in the range of 0 to 255.";
            Messages["ABSRespectDiag_InputRange", Language.Farsi] = "مقدار را در بازه 0 تا 255 وارد کنید.";

            Messages["ABSRespectDiag_MachineCondition", Language.English] = "The machine must be on a drum or flat surface. The switch must be off and the machine must be turned off.";
            Messages["ABSRespectDiag_MachineCondition", Language.Farsi] = "ماشین روی درام یا سطح صاف باشد. سوییچ باز و ماشین خاموش باشد.";

            Messages["ABSRespectDiag_BrakePedalPressure", Language.English] = "To achieve sufficient pressure in the hydraulic brake pedal modulator, the pedal must be pressed firmly throughout the entire bleeding process.";
            Messages["ABSRespectDiag_BrakePedalPressure", Language.Farsi] = "برای دستیابی به فشار کافی در مدولاتور هیدرولیک پدال ترمز، پدال باید در کل فرایند هواگیری به طور محکم فشرده شود.";

            Messages["ABSRespectDiag_BatteryVoltageCheck", Language.English] = "Please check the battery voltage. It must be ensured that the voltage is less than 10 volts.";
            Messages["ABSRespectDiag_BatteryVoltageCheck", Language.Farsi] = "لطفاً فشار باتری را بررسی کنید. باید تضمین شود که ولتاژ از ۱۰ ولت کمتر است.";

            Messages["ABSRespectDiag_RelayCheck", Language.English] = "Please check the valve relay and pump motor relay. The system can only perform the repair and bleeding procedure when the valve relay is active and the pump motor relay is inactive.";
            Messages["ABSRespectDiag_RelayCheck", Language.Farsi] = "لطفاً رله‌ی شیر و موتور پمپ را بررسی کنید. سیستم فقط زمانی می‌تواند روال تعمیر و تخلیه هوا را انجام دهد که رله شیر فعال و موتور پمپ غیرفعال باشد.";

            Messages["ABSRespectDiag_BleedingSequence", Language.English] = "The bleeding sequence must be performed in the following order: rear left (RL), front left (FL), front right (FR), rear right (RR). After completing each bleeding step, allow 5 minutes for the solenoid valve to cool down before proceeding to the next step.";
            Messages["ABSRespectDiag_BleedingSequence", Language.Farsi] = "توالی هواگیری باید به ترتیب زیر انجام شود: عقب چپ (RL)، جلو چپ (FL)، جلو راست (FR)، عقب راست (RR). پس از اتمام هر مرحله هواگیری، ۵ دقیقه به شیر برقی فرصت دهید تا خنک شود، سپس مرحله بعدی را انجام دهید.";

            Messages["ABSRespectDiag_OperationInProgress", Language.English] = "Operation is in progress.";
            Messages["ABSRespectDiag_OperationInProgress", Language.Farsi] = "عملیات در حال انجام است.";
            
            Messages["ABSRespectDiag_OperationSuccess", Language.English] = "The operation has been completed successfully.";
            Messages["ABSRespectDiag_OperationSuccess", Language.Farsi] = "عملیات با موفقیت به پایان رسید.";
            
            Messages["ABSRespectDiag_OperationFailed", Language.English] = "Operation failed.";
            Messages["ABSRespectDiag_OperationFailed", Language.Farsi] = "عملیات انجام نشد.";

            Messages["ABSRespectDiag_BleedingValveClose", Language.English] = "After completing the operation, close the corresponding bleed screw as long as air bubbles are still present in the brake fluid.";
            Messages["ABSRespectDiag_BleedingValveClose", Language.Farsi] = "پس از اتمام عملیات، تا زمانیکه حباب هوا در روغن ترمز موجود است، پیچ هواگیری مربوطه را ببندید.";

            Messages["ABSRespectDiag_BleedingDeviceRequired", Language.English] = "This operation requires a bleeding device and increased bleeding pressure.";
            Messages["ABSRespectDiag_BleedingDeviceRequired", Language.Farsi] = "این عملیات نیازمند دستگاه هواگیری و افزایش فشار هواگیری است.";

            Messages["ABSRespectDiag_BrakePedalPress", Language.English] = "Press and hold the brake pedal 4 times, each time for 3 seconds.";
            Messages["ABSRespectDiag_BrakePedalPress", Language.Farsi] = "پدال ترمز را ۴ بار و هر بار به مدت ۳ ثانیه نگه دارید.";
            
            Messages["ABSRespectDiag_FrontLeftComplete", Language.English] = "Front left completed.";
            Messages["ABSRespectDiag_FrontLeftComplete", Language.Farsi] = "جلو چپ تکمیل شد.";
           
            Messages["ABSRespectDiag_FrontRightComplete", Language.English] = "Front right (FR) bleeding procedure completed successfully.";
            Messages["ABSRespectDiag_FrontRightComplete", Language.Farsi] = "عملیات هواگیری جلو راست (FR) با موفقیت تکمیل شد.";
            
            Messages["ABSRespectDiag_RearRightComplete", Language.English] = "Rear right completed.";
            Messages["ABSRespectDiag_RearRightComplete", Language.Farsi] = "عقب راست تکمیل شد.";
            
            Messages["ABSRespectDiag_RearLeftComplete", Language.English] = "Rear left completed.";
            Messages["ABSRespectDiag_RearLeftComplete", Language.Farsi] = "عقب چپ تکمیل شد.";

            Messages["ABSRespectDiag_ConnectBleederDevice", Language.English] = "Connect the bleeding device and increase the bleeding pressure.";
            Messages["ABSRespectDiag_ConnectBleederDevice", Language.Farsi] = "دستگاه هواگیری را وصل کنید و فشار هواگیری را افزایش دهید.";
           
            Messages["ABSRespectDiag_PressPedalUntilBubbleFree", Language.English] = "Press the pedal until bubble-free fluid comes out of the bleed screw.";
            Messages["ABSRespectDiag_PressPedalUntilBubbleFree", Language.Farsi] = "تا زمانیکه روغن بدون حباب از پیچ هواگیری خارج شود، پدال را فشار دهید.";
           
            Messages["ABSRespectDiag_BleedingComplete", Language.English] = "Bleeding procedure completed. Turn off the device and disconnect it from the brake fluid reservoir. Adjust the brake fluid level in the TMC reservoir to the maximum mark.";
            Messages["ABSRespectDiag_BleedingComplete", Language.Farsi] = "مسیر هواگیری انجام شد. دستگاه را خاموش کنید و از مخزن روغن ترمز جدا کنید. سطح روغن ترمز را در مخزن TMC تا علامت حداکثر تنظیم کنید.";
        }
    }
}