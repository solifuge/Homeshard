using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCodex : MonoBehaviour
{
	public List<Item> codex = new List<Item>();

	List<string> wordListPerfectly;
	List<string> wordListGeneric;
	List<string> wordListObject;

    // Start is called before the first frame update
    void Start()
    {
    	Initialize();
    }

    void Initialize()
    {
		wordListPerfectly = new List<string>() {"Perfectly", "Absolutely", "Actively", "Actually", "Adequately", "All-In-All", "Altogether", "Bearably", "Blatantly", "Boldly", "Brazenly", "Candidly", "Completely", "Comprehensively", "Considerably", "Consummately", "Decidedly", "Deeply", "Devoutly", "Earnestly", "Enthusiastically", "Entirely", "Excessively", "Fairly", "Fearlessly", "Firmly", "Flagrantly", "Flat-Out", "Frankly", "Full-Blast", "Fully", "Generously", "Genuinely", "Honestly", "Kinda", "Liberally", "Moderately", "More-Or-Less", "Naturally", "Openly", "Ordinarily", "Passably", "Plainly", "Positively", "Powerfully", "Precisely", "Pretty", "Prevailingly", "Profoundly", "Purely", "Quite", "Rather", "Really", "Reasonably", "Relatively", "Resolutely", "Robustly", "Significantly", "Simply", "Sincerely", "Somewhat", "Stoutly", "Straightforwardly", "Thoroughly", "Toe-to-Tip", "Tolerably", "Totally", "Truly", "Truthfully", "Unmitigatedly", "Unwaveringly", "Utterly", "Very", "Vibrantly", "Vigorously", "Wantonly", "Well", "Wholeheartedly", "Wholly"};
		wordListGeneric = new List<string>() {"Generic", "Abstract", "All-Around", "Average", "Banal", "Basal", "Basic", "Broad", "Common", "Commonplace", "Comparable", "Comprehensive", "Conventional", "Customary", "Day-to-Day", "Default", "Dull", "Earthly", "Elemental", "Elementary", "Essential", "Everyday", "Familiar", "General", "Habitual", "Humdrum", "Ideal", "Inclusive", "Indefinite", "Inherent", "Innate", "Inspecific", "Intrinsic", "Lowly", "Miscellaneous", "Monotone", "Mundane", "Non-Specific", "Normal", "Ordinary", "Overall", "Pedestrian", "Primary", "Primitive", "Prosaic", "Quintessential", "Regular", "Routine", "Run-Of-The-Mill", "Standard", "Straightforward", "Sweeping", "Traditional", "Typical", "Ubiquitous", "Undetailed", "Unexceptional", "Uninteresting", "Universal", "Unspecific", "Usual", "Vague", "Vanilla", "Workaday", "Worldly"};
		wordListObject = new List<string>() {"Object", "Article", "Bit", "Commodity", "Component", "Constituent", "Doodad", "Doohickey", "Entity", "Gadget", "Gear", "Gizmo", "Goods", "Ingredient", "Item", "Material", "Matter", "Minutia", "Novelty", "Piece", "Scrap", "Something", "Substance", "Thing", "Thingamajig", "Unit", "Whatchamacallit", "Whatever", "Widget"};

    	Debug.Log ("Initializing Item Codex");
    	codex.Clear();
        codex.Add(new Item (0, "Perfectly Generic Object"));
        Debug.Log ("Item Added: " + codex[0].name + " (ID: " + codex[0].id + ")");

        // Get all Package subfolders inside the Packages folder
        foreach (string packagePath in Directory.GetDirectories(Application.dataPath + "/Packages/"))
 		{
 			Debug.Log ("Package Found at: " + packagePath);
			
			// Get paths to all .json files in the Items subfolder of each Package folder
			if(Directory.Exists(packagePath + "/Items/"))
			{
				foreach (string itemDataPath in Directory.GetFiles(packagePath + "/Items/", "*.json"))
		 		{
					Debug.Log (" > Importing Items from: " + itemDataPath);

					StreamReader reader = new StreamReader (itemDataPath);
					string rawItemData = reader.ReadToEnd();
					Debug.Log ("Contents: " + rawItemData);
					reader.Close();

					Item[] itemData = JsonConvert.DeserializeObject<Item[]>(rawItemData);

					foreach (Item i in itemData)
		 			{
		 				codex.Add(i);
		 				Debug.Log ("Item Added: " + i.name + " (ID: " + i.id + ")");
		 			}
		 		}
			}
 		}
    }

    Item GetItemByID(int _id)
    {
    	foreach (Item i in codex)
    	{
    		if (i.id == _id)
    		{
    			return i;
    		}
    	}

    	return null;
    }

    Item GetItemByName(string _name)
    {
    	foreach (Item i in codex)
    	{
    		if (i.name == _name)
    		{
    			return i;
    		}
    	}

    	return null;
    }

    string GenericNameByID(int _id)
    {
    	string genericName = wordListPerfectly[Calc.Wrap(_id, wordListPerfectly.Count - 1)];
    	genericName += " " + wordListGeneric[Calc.Wrap(_id, wordListGeneric.Count - 1)];
    	genericName += " " + wordListObject[Calc.Wrap(_id, wordListObject.Count - 1)];

    	return genericName;
    }
}

[System.Serializable]
public class Item
{
	public int id; //{get; set;}
	public string name; //{get; set;}

	public Item (int _id, string _name)
	{
		this.id = _id;
		this.name = _name;
	}
}

