﻿using System;

namespace Spray
{
	public class Gun
	{
		public static int[,] Ak47_Pattern = { { -4, 7 }, { 4, 19 }, { -3, 29 }, { -1, 31 }, { 13, 31 }, { 8, 28 }, { 13, 21 }, { -17, 12 }, { -42, 3 }, { -21, 2 }, { 12, 11 }, { -15, 7 }, { -26, -8 }, { -3, 4 }, { 40, 1 }, { 19, 7 }, { 14, 10 }, { 27, 0 }, { 33, -10 }, { -21, -2 }, { 7, 3 }, { -7, 9 }, { -8, 4 }, { 19, -3 }, { 5, 6 }, { -20, -1 }, { -33, -4 }, { -45, -21 }, { -14, 1 } };
		public static int[,] M4A4_Pattern = { { 2, 7 }, { 0, 9 }, { -6, 16 }, { 7, 21 }, { -9, 23 }, { -5, 27 }, { 16, 15 }, { 11, 13 }, { 22, 5 }, { -4, 11 }, { -18, 6 }, { -30, -4 }, { -24, 0 }, { -25, -6 }, { 0, 4 }, { 8, 4 }, { -11, 1 }, { -13, -2 }, { 2, 2 }, { 33, -1 }, { 10, 6 }, { 27, 3 }, { 10, 2 }, { 11, 0 }, { -12, 0 }, { 6, 5 }, { 4, 5 }, { 3, 1 }, { 4, -1 } };
		public static int[,] M4A1S_Pattern = { { 1, 6 }, { 0, 4 }, { -4, 14 }, { 4, 18 }, { -6, 21 }, { -4, 24 }, { 14, 14 }, { 8, 12 }, { 18, 5 }, { -4, 10 }, { -14, 5 }, { -25, -3 }, { -19, 0 }, { -22, -3 }, { 1, 3 }, { 8, 3 }, { -9, 1 }, { -13, -2 }, { 3, 2 }, { 1, 1 } };
		public static int[,] Gali_Pattern = { { 4, 4 }, { -2, 5 }, { 6, 10 }, { 12, 20 }, { -1, 25 }, { 2, 24 }, { 6, 18 }, { 11, 10 }, { -4, 14 }, { -22, 8 }, { -30, -10 }, { -29, -13 }, { -9, 8 }, { -12, 2 }, { -7, 1 }, { 0, 1 }, { 4, 7 }, { 25, 7 }, { 14, 4 }, { 25, -3 }, { 31, -9 }, { 6, 3 }, { -12, 3 }, { 10, -1 }, { 10, -1 }, { 10, -4 }, { -9, 5 }, { -32, -5 }, { -24, -3 }, { -15, 5 }, { 6, 8 }, { -14, -3 }, { -24, -5 }, { -13, -1 } };
		public static int[,] Famas_Pattern = { { -4, 5 }, { 1, 4 }, { -6, 10 }, { -1, 17 }, { 0, 20 }, { 14, 20 }, { 16, 20 }, { -6, 12 }, { -20, 8 }, { -16, 5 }, { -13, 2 }, { 4, 5 }, { 23, 4 }, { 12, 6 }, { 20, -3 }, { 5, 0 }, { 15, 0 }, { 3, 5 }, { -4, 3 }, { -25, -1 }, { -3, 2 }, { 11, 0 }, { 15, -7 }, { 15, -10 } };
		public static int[,] Ump45_Pattern = { { -1, 6 }, { -4, 8 }, { -2, 18 }, { -4, 23 }, { -9, 23 }, { -3, 26 }, { 11, 17 }, { -4, 12 }, { 9, 13 }, { 18, 8 }, { 15, 5 }, { -1, 3 }, { 5, 6 }, { 0, 6 }, { 9, -3 }, { 5, -1 }, { -12, 4 }, { -19, 1 }, { -1, -2 }, { 15, -5 }, { 17, -2 }, { -6, 3 }, { -20, -2 }, { -3, -1 } };
		public static int[,] Aug_Pattern = { { 5, 6 }, { 0, 13 }, { -5, 22 }, { -7, 26 }, { 5, 29 }, { 9, 30 }, { 14, 21 }, { 6, 15 }, { 14, 13 }, { -16, 11 }, { -5, 6 }, { 13, 0 }, { 1, 6 }, { -22, 5 }, { -38, -11 }, { -31, -13 }, { -3, 6 }, { -5, 5 }, { -9, 0 }, { 24, 1 }, { 32, 3 }, { 15, 6 }, { -5, 1 }, { 17, -3 }, { 29, -11 }, { 19, 0 }, { -16, 6 }, { -16, 3 }, { -4, 1 } };
		public static int[,] Sg_Pattern = { { -4, 9 }, { -13, 15 }, { -9, 25 }, { -6, 29 }, { -8, 31 }, { -7, 36 }, { -20, 14 }, { 14, 17 }, { -8, 12 }, { -15, 8 }, { -5, 5 }, { 6, 5 }, { -8, 6 }, { 2, 11 }, { -14, -6 }, { -20, -17 }, { -18, -9 }, { -8, -2 }, { 41, 3 }, { 56, -5 }, { 43, -1 }, { 18, 9 }, { 14, 9 }, { 6, 7 }, { 21, -3 }, { 29, -4 }, { -6, 8 }, { -15, 5 }, { -38, -5 } };


		private readonly int[,] pattern;
		private readonly String name;

		private Gun(String name, int[,] pattern)
		{
			this.name = name;
			this.pattern = pattern;
		}
		public String Name {
			get { return name; }
		}
		public int[,] Pattern {
			get { return pattern; }
		}

		public static Gun Ak47 = new Gun("AK-47", Ak47_Pattern);
		public static Gun M4a4 = new Gun("M4A4", M4A4_Pattern);
		public static Gun M4a1s = new Gun("M4A1-S", M4A1S_Pattern);
		public static Gun Gali = new Gun("GALI", Gali_Pattern);
		public static Gun Famas = new Gun("FAMAS", Famas_Pattern);
		public static Gun Ump45 = new Gun("UMP-45", Ump45_Pattern);
		public static Gun Aug = new Gun("AUG", Aug_Pattern);
		public static Gun Sg = new Gun("SG", Sg_Pattern);
		
	}
}
