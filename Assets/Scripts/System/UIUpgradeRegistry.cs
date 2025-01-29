using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;


public class UpgradeIdentifer
{
    public UpgradeType upgradeType;
    public int index;
    public UpgradeIdentifer(UpgradeType uType, int indexer)
    {
        upgradeType = uType;
        index = indexer;
    }

}
// Try to automize later
public class UIUpgradeRegistry : PersistentSingleton<UIUpgradeRegistry>
{
    [SerializeField] private UpgradeEntry[] uE;

    [SerializeField] private Dictionary<UpgradeIdentifer, UpgradeEntry> upgradeEntries;
    private void Start()
    {
        upgradeEntries = new Dictionary<UpgradeIdentifer, UpgradeEntry>();
        for (int i = 0; i < uE.Length; i++)
        {
            UpgradeIdentifer uI = new(uE[i].UType, uE[i].Index);

            upgradeEntries.Add(uI, uE[i]);
        }
    }

    public UpgradeEntry GetEntry(UpgradeType uType, int index)
    {
        UpgradeIdentifer uI = new(uType, index);

        return upgradeEntries[uI];
    }
}
