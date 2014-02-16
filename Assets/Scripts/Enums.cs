using UnityEngine;
using System.Collections;

public enum GuiltLevel
{
	guilty=1,
	suspect,
	witness,
	unrelated = -1
};

public enum Category
{
	None = 0,			//Implies that the npc has no ability to use weapons
	SmallProjectiles=1,	//Like darts and other small thrown objects
	BluntWeapons =2, 		//Melee weapon
	BladedWeapons=3,		//Swords
	Poison=4,	//Poisons
	Miscellaneous=5,		//Objects not normally associated with weaponry
	Firearms=6,			//Guns are dangerous
	
	PersonalItem=-1,		//An object with this category is not used for killing but is associated with a character
	unrelated = -2
};