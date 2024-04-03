using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance { get; private set; }

    public GameObject npcPrefab;

    private int _visitorCount, _visitorMax;
    private bool _spawning, _randomEvents;

    private Dictionary<Guid, NPC> _npcs;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public int GetVisitorCount()
    {
        return _visitorCount;
    }

    public void Start()
    {
        _visitorMax = 20;
        _visitorCount = 0;

        _npcs = new Dictionary<Guid, NPC>();

        _spawning = true;
        _randomEvents = true;

        StartCoroutine(TryToSpawn());
        StartCoroutine(RandomEvents());
    }

    public GameObject FindFriend(Guid npcID)
    {
        var groupID = _npcs[npcID].GetGroupID();
        return _npcs.Values.ToList().Find(npc => npc.GetGroupID() == groupID && npc.GetID() != npcID).GetGameObject();
    }

    private IEnumerator TryToSpawn()
    {
        while (_spawning)
        {
            // Generate 1-3 people at a time.
            var availableSpace = _visitorMax - _visitorCount;
            if (availableSpace > 0)
            {
                var numberOfPeople = Random.Range(1, availableSpace < 4 ? availableSpace : 4);
                var groupID = Guid.NewGuid();
                var randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

                for (var i = 0; i < numberOfPeople; i++)
                {
                    var npcGameObject = Instantiate(npcPrefab, new Vector3(-26, 12, 0), Quaternion.identity);
                    npcGameObject.GetComponent<SpriteRenderer>().color = randomColor;

                    var npcScript = npcGameObject.GetComponent<NPCScript>();
                    npcScript.ID = Guid.NewGuid();

                    var npc = new NPC(npcScript, groupID);
                    _npcs.Add(npcScript.ID, npc);

                    _visitorCount++;
                }
            }

            // Wait 10-30 seconds.
            yield return new WaitForSeconds(Random.Range(10f, 30f));
        }
    }

    private IEnumerator RandomEvents()
    {
        while (_randomEvents)
        {
            if (_npcs.Count > 1)
            {
                // Select a random NPC as the "chaser".
                var npcIDs = _npcs.Keys.ToList();
                var randomChaserNPC = _npcs[npcIDs[Random.Range(0, npcIDs.Count)]];

                try
                {
                    // Select a different NPC as the "target".
                    var targetNPC = _npcs.Values.ToList().Find(npc =>
                        npc.GetGroupID() == randomChaserNPC.GetGroupID() && npc.GetID() != randomChaserNPC.GetID());

                    // Assign the target NPC's GameObject as the MovingTarget of the chaser NPC.
                    var chaserScript = randomChaserNPC.GetScript();
                    chaserScript.MovingTarget = targetNPC.GetGameObject();
                }
                catch (NullReferenceException nre)
                {
                }
            }

            yield return new WaitForSeconds(Random.Range(5f, 20f));
        }
    }
}

public class NPC
{
    private NPCScript _npc;
    private Guid _groupID;

    public NPC(NPCScript npc, Guid groupID)
    {
        _npc = npc;
        _groupID = groupID;
    }

    public Guid GetID()
    {
        return _npc.ID;
    }

    public GameObject GetGameObject()
    {
        return _npc.gameObject;
    }

    public NPCScript GetScript()
    {
        return _npc;
    }

    public Guid GetGroupID()
    {
        return _groupID;
    }
}