#include "Radar_Respect_Diag.h"//delshadi
#include "MSG_Radar_Respect_Diag.h"
#include "ArrayTools.h"
#include "OtherHeader\Radar_Respect_Diag_OH.h"
Radar_Respect_Diag::Radar_Respect_Diag(void)
{
}
Radar_Respect_Diag::~Radar_Respect_Diag(void)
{
}
bool Radar_Respect_Diag::RADARDTC(CommandAdorner* Command)
{
    byte res[1424];
	 
    int res_len = 0;
    memset(res, 0, sizeof(res));
    u32 code = 0;
    byte send_buff[3] = { 0x19, 0x02, 0xFF };
    FaultCodeAdorner _FaultCodeAdorner;

    res_len =  (int)OBDii.Send_Receive(sizeof(send_buff), send_buff, res, sizeof(res));

    if (res_len < 7 || res[0] == 0x7F)
    {
        Manager.BreakFlag = true;
        Command->State.IsFaulty = true;
        return true;
    }

    for (int i = 3; (i + 3) < res_len; i += 4)
    {
      code = 0;
			code |= (res[i] << 16);
			code |= (res[i + 1] << 8);
			code |= res[i + 2];

        if (code == 0xFFFF || code == 0x0000)
            continue;

        byte state = res[i + 3];

        if (_FaultCodeAdorner.CheckFault(code, state, Command))
        {
           if (((state & 0x09) == 0x09))
            {
                _FaultCodeAdorner.AddFault(code,null,GetDTCCategory(code, Command->DB->FaultCodeLength),GetDTCinCategoryMode(code,Command->DB->FaultCodeLength));
            }
        }
    }

    _FaultCodeAdorner.ShowFaults(Command, true);
    Manager.BreakFlag = true;
    return true;
}
