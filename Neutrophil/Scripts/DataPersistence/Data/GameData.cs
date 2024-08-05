using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector3 playerPosition;
    public Vector3 respawnPosition;
    public AttributesData playerAttributesData;

    // nilai default
    public GameData() 
    {
        playerPosition = Vector3.zero;
        respawnPosition = Vector3.zero;
        playerAttributesData = new AttributesData();
    }

    // Menghitung persentase penyelesaian game
    public int GetPercentageComplete() 
    {
        // Implementasikan logika sesuai kebutuhan permainan
        // Misalnya, hitung persentase berdasarkan kemajuan misi atau level
        
        // Contoh sederhana: Hitung persentase berdasarkan kemajuan misi
        // Misalkan terdapat total 10 misi dan pemain telah menyelesaikan 3 misi
        int totalMissions = 10;
        int completedMissions = 3;

        // Hitung persentase berdasarkan jumlah misi yang diselesaikan
        float percentage = (float)completedMissions / totalMissions * 100;
        
        // Ubah nilai float ke int (pembulatan ke bawah)
        return Mathf.FloorToInt(percentage);
    }
}
