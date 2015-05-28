﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("SectionEnd"))
        {
            // Get spawn position
            var spawnPosition = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
            var stageSection = Game.instance.GetSection();
            var spawnedObj = Instantiate(stageSection.prefab, spawnPosition, Quaternion.identity);
            Game.instance.PlaceSectionInActiveList(spawnedObj.name, stageSection);
        }
    }

}