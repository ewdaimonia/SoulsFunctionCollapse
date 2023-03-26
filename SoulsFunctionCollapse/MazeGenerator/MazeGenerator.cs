using Erd_Tools;
using PropertyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoulsFunctionCollapse.MazeGenerator
{
    public class MazeGenerator
    {
        ErdHook erdHook;

        public MazeGenerator() { 
            erdHook = new ErdHook(5000, 5000, p => p.ProcessName == "EldenRing");
            erdHook.Start();
        }

        Dictionary<string, int> namesToEnts = new Dictionary<string, int>() {
            { "cliff 0", 1061288000 },
            { "cliff 1", 1061288001 },
            { "cliff 2", 1061288002 },
            { "cliff 3", 1061288003 },
            { "cliffcorner 0", 1061288004 },
            { "cliffcorner 1", 1061288005 },
            { "cliffcorner 2", 1061288006 },
            { "cliffcorner 3", 1061288007 },
            { "cliffturn 0", 1061288008 },
            { "cliffturn 1", 1061288009 },
            { "cliffturn 2", 1061288010 },
            { "cliffturn 3", 1061288011 },
            { "grass 0", 1061288012 },
            { "grasscorner 0", 1061288013 },
            { "grasscorner 1", 1061288014 },
            { "grasscorner 2", 1061288015 },
            { "grasscorner 3", 1061288016 },
            { "road 0", 1061288017 },
            { "road 1", 1061288018 },
            { "road 2", 1061288019 },
            { "road 3", 1061288020 },
            { "roadturn 0", 1061288021 },
            { "roadturn 1", 1061288022 },
            { "roadturn 2", 1061288023 },
            { "roadturn 3", 1061288024 },
            { "water_a 0", 1061288025 },
            { "water_a 1", 1061288026 },
            { "water_a 2", 1061288027 },
            { "water_a 3", 1061288028 },
            { "watercorner 0", 1061288029 },
            { "watercorner 1", 1061288030 },
            { "watercorner 2", 1061288031 },
            { "watercorner 3", 1061288032 },
            { "waterside 0", 1061288033 },
            { "waterside 1", 1061288034 },
            { "waterside 2", 1061288035 },
            { "waterside 3", 1061288036 },
            { "waterturn 0", 1061288037 },
            { "waterturn 1", 1061288038 },
            { "waterturn 2", 1061288039 },
            { "waterturn 3", 1061288040 },
            { "water_b 0", 1061288041 },
            { "water_b 1", 1061288042 },
            { "water_b 2", 1061288043 },
            { "water_b 3", 1061288044 },
            { "water_c 0", 1061288045 },
            { "water_c 1", 1061288046 },
            { "water_c 2", 1061288047 },
            { "water_c 3", 1061288048 },
        };

        public void Generate() {
            if (erdHook.Hooked) {
                Model model;
                List<List<string>> tileNames = new List<List<string>>();

                model = new SimpleTiledModel("Summer", "grass", 10, 10, true, true, Model.Heuristic.Entropy);

                if (model is SimpleTiledModel stmodel)
                {
                    tileNames = stmodel.ListOutput();
                }

                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        //wait for can generate map flag
                        bool canContinue = false;
                        while (!canContinue)
                        {
                            canContinue = erdHook.IsEventFlag(1061287901);
                            Thread.Sleep(100);
                        }

                        int assetId = namesToEnts[tileNames[y][x]];

                        erdHook.SetEventFlag(assetId, true);
                        erdHook.SetEventFlag(1061287900, true);
                        Thread.Sleep(100);
                    }
                }
            }
        }

        ~MazeGenerator()
        {
            erdHook.Stop();
        }
    }
}
