using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config {

	public static Color[] ColorPool = new Color[6];

	public static int MixColor(int i, int j) {
		if (i == j) {
			return i;
		} else {
			if (i > j) {
				int tmp = i;
				i = j;
				j = tmp;
			}

			if (i == 0) {
				if (j == 1) {
					return 3;
				} else {
					return 4;
				}
			} else {
				return 5;
			}
		}
	}
}
