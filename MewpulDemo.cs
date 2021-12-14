using Azure;
using Azure.DigitalTwins.Core;
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwinsClientApp
{
    public class MewpulDemo
    {
        private readonly CommandLoop cl;
        public MewpulDemo(CommandLoop cl)
        {
            this.cl = cl;
        }

        public async Task InitBuilding()
        {
            Log.Alert($"Deleting all twins...");
            await cl.DeleteAllTwinsAsync();
            Log.Out($"Creating 3 Mewpul...");
            await InitializeGraph();
        }

        private async Task InitializeGraph()
        {
            string[] modelsToUpload = new string[] {"CreateModels", "MewpulBuilding","WetLab","DryLab"};
            Log.Out($"Uploading {string.Join(", ", modelsToUpload)} models");
            await cl.CommandCreateModels(modelsToUpload);

            Log.Out($"Creating Twins...");
            await cl.CommandCreateDigitalTwin(new string[23]
                {
                    "CreateTwin", "dtmi:digitaltwins:building:MewpulBuilding;1", "Address",
                    "displayName","Address",
                    "region", "string","true",
                    "postalCode","string","true",
                    "country","string","true",
                    "city","string","true",
                    "addressLine1","string","true",
                    "addressLine2","string","true"
                });

            await cl.CommandCreateDigitalTwin(new string[60]
                {   
                    "CreateTwin", "dtmi:digitaltwins:room:LaboratoryWet;1", "WetLab",
                    "displayName", "WetLaboratory",
                    "humiditySensor","double","true","percent",
                    "humiditySentpoint","double","true","percent",
                    "humidityDelta","double","true","percent",
                    "temperatureSensor","double","true",
                    "temperatureSetpoint","double","true",
                    "temperatureDelta","double","true",
                    "CO2Sensor","double","true",
                    "CO2Setpoint","double","true",
                    "CO2Delta","double","true",
                    "dwellTimeAverage", "double","true","second",
                    "peopleCount","double","true",
                    "hasInferredOccupancy", "boolean","true",
                    "isOccupied", "boolean","true",
                    "maxOccupancy","integer","true","Building Code Occupancy",
                    "grossArea","double","true","squareFoot",
                    "usableArea","double","true","squareFoot"
                 });
            
              await cl.CommandCreateDigitalTwin(new string[60]
                {
                    "CreateTwin", "dtmi:digitaltwins:room:LaboratoryDry;1", "DryLab",
                    "displayName", "DryLaboratory",
                    "humiditySensor","double","true","percent",
                    "humiditySentpoint","double","true","percent",
                    "humidityDelta","double","true","percent",
                    "temperatureSensor","double","true",
                    "temperatureSetpoint","double","true",
                    "temperatureDelta","double","true",
                    "CO2Sensor","double","true",
                    "CO2Setpoint","double","true",
                    "CO2Delta","double","true",
                    "dwellTimeAverage", "double","true","second",
                    "peopleCount","double","true",
                    "hasInferredOccupancy", "boolean","true",
                    "isOccupied", "boolean","true",
                    "maxOccupancy","integer","true","Building Code Occupancy",
                    "grossArea","double","true","squareFoot",
                    "usableArea","double","true","squareFoot"
                 });

             Log.Out($"Creating edges between them");
             await cl.CommandCreateRelationship(new string[4]
                 {
                     "CreateEdge", "WetLab", "isPartOf", "MewpulBuilding"
                 });
            await cl.CommandCreateRelationship(new string[4]
                 {
                     "CreateEdge", "DryLab", "isPartOf", "MewpulBuilding"
                 });
        }
    }
}