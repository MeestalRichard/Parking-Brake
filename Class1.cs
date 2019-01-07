using System;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;


public class Parking_Brake : Script
{
    private Ped playerPed = Game.Player.Character;
    private Vehicle veh = Game.Player.Character.CurrentVehicle;

    private bool parkingBrakeOn;
    private ScriptSettings config;
    private Keys Parking_Brake_Button;
    private bool ParkingBrakeOn = false;
    private bool Parking_Brake_Lights = false;
    private bool Parking_Brake_Active_Message = false;


    public Parking_Brake()

    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        ReadINI();

    }

    private void ReadINI()
    {
            config = ScriptSettings.Load("scripts\\Parking Brake.ini");
            Parking_Brake_Button = config.GetValue<Keys>("Options", "Parking_Brake", Keys.B);
            Parking_Brake_Active_Message = config.GetValue<bool>("Options", "Parking_Brake_Active_message", false);
            Parking_Brake_Lights = config.GetValue<bool>("Options", "Parking_Brake_Lights", false); 
    }


    private void OnTick(object sender, EventArgs e)
    {
        if (veh.Model.IsCar || veh.Model.IsBike)
        {
            if (playerPed.IsAlive && playerPed.IsInVehicle())
            {
                parkingBrakeOn = ParkingBrakeOn;
                if (parkingBrakeOn)
                {
                    Function.Call(Hash.SET_VEHICLE_HANDBRAKE, playerPed.CurrentVehicle, 1);
                }

                bool parking_Brake_Lights = Parking_Brake_Lights;
                if (parking_Brake_Lights)
                {
                    parkingBrakeOn = ParkingBrakeOn;
                    if (parkingBrakeOn)
                    {
                        Function.Call(Hash.SET_VEHICLE_BRAKE_LIGHTS, playerPed.CurrentVehicle, 1);
                    }

                    bool parkingBrakeOn3 = ParkingBrakeOn;
                    if (parkingBrakeOn3)
                    {
                        Function.Call(Hash.SET_VEHICLE_BRAKE_LIGHTS, playerPed.CurrentVehicle, 0);
                    }
                }
            }

            if (!ParkingBrakeOn)
            {
                    Function.Call(Hash.SET_VEHICLE_HANDBRAKE, playerPed.CurrentVehicle, 0);
            }
        }
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        ReadINI();
        if (e.KeyCode == Parking_Brake_Button)
            if (veh.Model.IsCar || veh.Model.IsBike)
            {
                if (playerPed.IsAlive && playerPed.IsInVehicle())
                {
                    ParkingBrakeOn = !ParkingBrakeOn;
                }
            }
    }
}