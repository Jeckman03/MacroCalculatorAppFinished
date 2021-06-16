using System;

class MainClass {
  public static void Main (string[] args) {

		PersonModel person = GetUserInfo();

		CalculateNumbers(person);

		DisplayUserMacroInfo(person);


    Console.ReadLine();
  }

	private static PersonModel GetUserInfo()
	{
		PersonModel person = new PersonModel();

		Console.Write("What is your name: ");
		person.Name = Console.ReadLine();
		Console.Clear();

		Console.Write("What is you age: ");
		person.Age = Convert.ToInt32(Console.ReadLine());
		Console.Clear();

		Console.Write("What is your current weight: ");
		person.Weight = Convert.ToDouble(Console.ReadLine());
		Console.Clear();

		
		Console.WriteLine("1. Male");
		Console.WriteLine("2. Female");
		Console.WriteLine();
		Console.Write("What is your gender M/F: ");
		string gender = Console.ReadLine();

		if (gender.ToLower() == "m")
		{
			person.Gender = Sex.Male;
		}
		else
		{
			person.Gender = Sex.Female;
		}
		Console.Clear();

		Console.Write("What is your height in inches: ");
		person.HeightInches = Convert.ToDouble(Console.ReadLine());
		Console.Clear();
		
		Console.WriteLine("1. Lose Weight");
		Console.WriteLine("2. Maintain Weight");
		Console.WriteLine("3. Gain Weight");
		Console.WriteLine();
		Console.Write("What is your goal: ");
		string goal = Console.ReadLine();

		if (goal.ToLower() == "1")
		{
			person.WeightGoal = Goal.WeightLoss;
		}
		else if (goal.ToLower() == "2")
		{
			person.WeightGoal = Goal.MaintainWeight;
		}

		else
		{
			person.WeightGoal = Goal.GainWeight;
		}
		Console.Clear();
		
		
		Console.WriteLine("1. Sedentary (little to no exercise + work a desk job)");
		Console.WriteLine("2. Lightly Active (light exercise 1-3 days / week)");
		Console.WriteLine("3. Moderately Active (moderate exercise 3-5 days / week)");
		Console.WriteLine("4. Very Active (heavy exercise 6-7 days / week)");
		Console.WriteLine("5. Extremely Active (very heavy exercise, hard labor job, training 2x / day)");
		Console.WriteLine();
		Console.Write("What is your activity level: ");
		string activity = Console.ReadLine();

		if (activity == "1")
		{
			person.Activity = ActivityLevel.Sedentary;
		}
		else if (activity == "2")
		{
			person.Activity = ActivityLevel.Light;

		}
		else if (activity == "3")
		{
			person.Activity = ActivityLevel.Moderate;

		}
		else if (activity == "4")
		{
			person.Activity = ActivityLevel.Very;

		}
		else
		{
			person.Activity = ActivityLevel.Extremely;

		}
		Console.Clear();

		
		Console.WriteLine("1. Low Carbs");
		Console.WriteLine("2. Balanced");
		Console.WriteLine("3. High Carbs");
		Console.WriteLine();
		Console.Write("What is your carb preferense: ");
		string carbs = Console.ReadLine();

		if (carbs == "1")
		{
			person.Preferense = CarbPreferense.Low;
		}
		else if (carbs == "2")
		{
			person.Preferense = CarbPreferense.Balanced;

		}
		else
		{
			person.Preferense = CarbPreferense.High;

		}
		Console.Clear();

		
		return person;
	}

	private static void CalculateNumbers(PersonModel person)
	{
		Calculations.GetBMR(person);
		Calculations.GetTDEE(person);
		Calculations.GetCalorieDeficit(person);
		Calculations.GetMacros(person);
	}

	private static void DisplayUserMacroInfo(PersonModel person)
	{
		Console.WriteLine("Here are you stats: ");
		Console.WriteLine();
		Console.WriteLine($"BMR: { person.BMR }");
		Console.WriteLine($"TDEE: { person.TDEE}");
		Console.WriteLine($"Calorie Deficit: { person.CalorieDeficit }");
		Console.WriteLine($"Macros: Fat: { person.Fat } Protien: { person.Protien } Carbs: { person.Carbs }");
	}

}

public class MacroModel
{
	public double Fat { get; set; }
	public double Carbs { get; set; }
	public double Protien { get; set; }

	public double Calories { get; set; }
}

public class PersonalMacros : MacroModel
{
	public double BMR { get; set; }
	public double TDEE { get; set; }
	public double LBM { get; set; }
	public double CalorieDeficit { get; set; }
	public double CalorieSurplus { get; set; }

}

public class PersonModel : PersonalMacros
{
	public string Name { get; set; }
	public int Age { get; set; }
	public double HeightInches { get; set; }
	public double Weight { get; set; }
	public Sex Gender { get; set; }
	public ActivityLevel Activity { get; set; }
	public Goal WeightGoal { get; set; }
	public CarbPreferense Preferense { get; set; }
}



public static class Calculations
{
	public static void GetTDEE(PersonModel person)
	{
		double multiplier;

		if (person.Activity == ActivityLevel.Sedentary)
		{
		//  Sedentary (little to no exercise + work a desk job) = 1.2
			multiplier = 1.2;
		}

		else if (person.Activity == ActivityLevel.Light)
		{
		//  Lightly Active (light exercise 1-3 days / week) = 1.375
			multiplier = 1.375;
		}

		else if (person.Activity == ActivityLevel.Moderate)
		{
		//  Moderately Active (moderate exercise 3-5 days / week) = 1.55
			multiplier = 1.55;
		}

		else if (person.Activity == ActivityLevel.Very)
		{
		//  Very Active (heavy exercise 6-7 days / week) = 1.725
			multiplier = 1.725;
		}
		else
		{
		//  Extremely Active (very heavy exercise, hard labor job, training 2x / day)=  1.9
			multiplier = 1.9;
		}

		double tdee = person.BMR * multiplier;
		person.TDEE = Math.Ceiling(tdee);
	}

	public static void GetBMR(PersonModel person)
	{
		double bmr;

		if (person.Gender == Sex.Male)
		{
			// Men: BMR = 66 + ( 6.2 × weight in pounds ) + ( 12.7 × height in inches ) – ( 6.76 × age in years )

			bmr = 66 + (6.2 * person.Weight) + (12.7 * person.HeightInches) - (6.76 * person.Age);
		}
		else
		{
			// Female: BMR = 655 + ( 4.35 × weight in pounds ) + ( 4.7 × height in inches ) - ( 4.7 × age in years )

			bmr = 655 + (4.35 * person.Weight) + (4.7 * person.HeightInches) - (4.7 * person.Age);
		}

		person.BMR = Math.Ceiling(bmr); 
		
	}

	public static void GetLBM(PersonModel person)
	{
		// Note: 1lb = 0.453592kg, and 1in = 2.54cm
		double weightKG = person.Weight * 0.453592;
		double heightCM = person.HeightInches * 2.54;
		double LBMKGS;

		if (person.Gender == Sex.Male)
		{
			// Men: Lean body mass = (0.32810 × W) + (0.33929 × H) − 29.5336
			LBMKGS = (0.32810 * weightKG) + (0.33929 * heightCM) - 29.5336;
			person.LBM = LBMKGS * 2.205;
		}
		else
		{
			// For women: Lean body mass = (0.29569 × W) + (0.41813 × H) − 43.2933
			LBMKGS = (0.29569 * weightKG) + (0.41813 * heightCM) - 43.2933;
			person.LBM = LBMKGS * 2.205;
		}

	}

	public static void GetCalorieDeficit(PersonModel person)
	{
		double deficit = person.TDEE - (person.TDEE * 0.20);
		person.CalorieDeficit = Math.Ceiling(deficit);
	}

	public static void GetMacros(PersonModel person)
	{
		GetLBM(person);
		person.Protien = Math.Ceiling((person.LBM * 1.2));

		double leftOverCalories = person.CalorieDeficit - (person.Protien * 4);

		if (person.Preferense == CarbPreferense.Low)
		{
			person.Fat = Math.Ceiling((leftOverCalories * 0.60) / 9);
			person.Carbs = Math.Ceiling((leftOverCalories * 0.40) / 4);
		} 
		else if (person.Preferense == CarbPreferense.Balanced)
		{
			person.Fat = Math.Ceiling((leftOverCalories * 0.40) / 9);
			person.Carbs = Math.Ceiling((leftOverCalories * 0.60) / 4);
		}
		else
		{
			person.Fat = Math.Ceiling((leftOverCalories * 0.30) / 9);
			person.Carbs = Math.Ceiling((leftOverCalories * 0.70) / 4);
		}

		// Split up the CalorieDeficit into GetMacros
		// Break the calories up into three percentages depening on user preferense
			// Standard is 40/40/20: 20 being fat
		// 1 Fat is 9 Calories
		// 1 Carb and Protien is 4 Calories
	}
}

public enum CarbPreferense
{
	Low,
	Balanced,
	High
}

public enum Goal
{
	WeightLoss,
	MaintainWeight,
	GainWeight
}

public enum ActivityLevel
{
	Sedentary,
	Light,
	Moderate,
	Very,
	Extremely
}

public enum Sex
{
	Male,
	Female
}