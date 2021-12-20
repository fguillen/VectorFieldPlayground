using UnityEngine;

public static class PerlinNoise3D
{
    // From: https://gist.github.com/tntmeijs/6a3b4587ff7d38a6fa63e13f9d0ac46d
    // See parameters explanations here: https://docs.unity3d.com/Packages/com.unity.visualeffectgraph@10.3/manual/Operator-ValueCurlNoise.html
    public static float value(float x, float y, float z, float seed = 1, float frequency = 1f, float amplitude = 1f, float persistence = 1f, int octave = 1)
	{
		float noise = 0.0f;

		for (int i = 0; i < octave; ++i)
		{
			// Get all permutations of noise for each individual axis
			float noiseXY = Mathf.PerlinNoise(x * frequency + seed, y * frequency + seed) * amplitude;
			float noiseXZ = Mathf.PerlinNoise(x * frequency + seed, z * frequency + seed) * amplitude;
			float noiseYZ = Mathf.PerlinNoise(y * frequency + seed, z * frequency + seed) * amplitude;

			// Reverse of the permutations of noise for each individual axis
			float noiseYX = Mathf.PerlinNoise(y * frequency + seed, x * frequency + seed) * amplitude;
			float noiseZX = Mathf.PerlinNoise(z * frequency + seed, x * frequency + seed) * amplitude;
			float noiseZY = Mathf.PerlinNoise(z * frequency + seed, y * frequency + seed) * amplitude;

			// Use the average of the noise functions
			noise += (noiseXY + noiseXZ + noiseYZ + noiseYX + noiseZX + noiseZY) / 6.0f;

			amplitude *= persistence;
			frequency *= 2.0f;
		}

		// Use the average of all octaves
		return noise / octave;
	}
}
