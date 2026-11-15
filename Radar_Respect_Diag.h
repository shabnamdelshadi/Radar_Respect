#ifndef __RADAR_RESPECT_DIAG_H
#define __RADAR_RESPECT_DIAG_H

#include "rayanmessage.h"
#include "Algorithm.h"

class Radar_Respect_Diag : public BaseAlgorithm
{
public:
	virtual bool InternalSwitchCase(CommandAdorner* Command);
	const static u16 Class_ID;
	Radar_Respect_Diag(void);
	~Radar_Respect_Diag(void);

	bool RADARDTC(CommandAdorner* Command);
	bool YawSensorCalibration(CommandAdorner* Command);
	bool RepairBleedBosch(CommandAdorner* Command);
	bool RepairBleed_MK100(CommandAdorner* Command);
	bool FPBReplacement(CommandAdorner* Command);
	bool ManualBleeding(CommandAdorner* Command);
	bool HydrolicAirVolume(CommandAdorner* Command);
	bool HydrolicAirSpeed(CommandAdorner* Command);
	bool PlungerairVolume(CommandAdorner* Command);
	bool HydrolicLeakVolume(CommandAdorner* Command);
	bool HydrolicLeakSpeed(CommandAdorner* Command);
	bool RepairBleed(CommandAdorner* Command);
	bool InputrodPos100(CommandAdorner* Command);
	bool InputrodPos200(CommandAdorner* Command);
	bool TMCPressP100(CommandAdorner* Command);
	bool TMCPressP200(CommandAdorner* Command);

	bool Keyboard_Actuator(CommandAdorner* Command);
	bool Delay_Actuator(CommandAdorner* Command);
	


private:
	bool delay_exitsuported(u32 delay, char* msg, bool show_timer);
	
};
#endif
