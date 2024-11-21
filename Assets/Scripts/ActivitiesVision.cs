using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivitiesVision : MonoBehaviour
{
    public GameObject[] Activities;
    [SerializeField] Stats Stats;
    [SerializeField] GameManager gameManager;

    public void CleanProgressBar()
    {
        foreach (var item in Activities)
        {
            item.GetComponent<Activity>().CleanActivityProgress();
        }
    }

    public void CheckActivityVision()
    {
        if (gameManager.currentLevel == GameManager.Level.Capsule)
        {
            var Concentrate = Stats.activities.FirstOrDefault(n => n.name == "Ñoncentrate");
            var concentrateActivity = Activities.FirstOrDefault(n => n.name == "Ñoncentrate");

            var Recover = Stats.activities.FirstOrDefault(n => n.name == "Recover");
            var recoverActivity = Activities.FirstOrDefault(n => n.name == "Recover");

            var InvestigateCapsule = Stats.activities.FirstOrDefault(n => n.name == "InvestigateCapsule");
            var investigateCapsuleActivity = Activities.FirstOrDefault(n => n.name == "InvestigateCapsule");

            var BrokeCapsuleDoor = Stats.activities.FirstOrDefault(n => n.name == "BrokeCapsuleDoor");
            var brokeCapsuleDoorActivity = Activities.FirstOrDefault(n => n.name == "BrokeCapsuleDoor");

            var RecoverCapsuleButton = Stats.activities.FirstOrDefault(n => n.name == "RecoverCapsuleButton");
            var recoverCapsuleButtonActivity = Activities.FirstOrDefault(n => n.name == "RecoverCapsuleButton");

            var RepeatInvestigateCapsule = Stats.activities.FirstOrDefault(n => n.name == "RepeatInvestigateCapsule");
            var repeatInvestigateCapsuleActivity = Activities.FirstOrDefault(n => n.name == "RepeatInvestigateCapsule");

            var O2Mask = Stats.activities.FirstOrDefault(n => n.name == "O2Mask");
            var O2MaskActivity = Activities.FirstOrDefault(n => n.name == "O2Mask");

            var TakeO2Mask = Stats.activities.FirstOrDefault(n => n.name == "TakeO2Mask");
            var TakeO2MaskActivity = Activities.FirstOrDefault(n => n.name == "TakeO2Mask");

            var ExitFromCapsule = Stats.activities.FirstOrDefault(n => n.name == "ExitFromCapsule");
            var ExitFromCapsuleActivity = Activities.FirstOrDefault(n => n.name == "ExitFromCapsule");

            concentrateActivity.SetActive(Concentrate.completed != Concentrate.completedMax);

            recoverActivity.SetActive(Recover.completed != Recover.completedMax);

            if (Concentrate.completed == Concentrate.completedMax && Recover.completed == Recover.completedMax
                && InvestigateCapsule.completed != InvestigateCapsule.completedMax)
            {
                investigateCapsuleActivity.SetActive(true);
            }
            else
            {
                investigateCapsuleActivity.SetActive(false);
            }

            if (InvestigateCapsule.completed == InvestigateCapsule.completedMax)
            {
                brokeCapsuleDoorActivity.SetActive(true);
                recoverCapsuleButtonActivity.SetActive(true);
                repeatInvestigateCapsuleActivity.SetActive(true);
            }
            else
            {
                brokeCapsuleDoorActivity.SetActive(false);
                recoverCapsuleButtonActivity.SetActive(false);
                repeatInvestigateCapsuleActivity.SetActive(false);
            }

            if (BrokeCapsuleDoor.completed == BrokeCapsuleDoor.completedMax || RecoverCapsuleButton.completed == RecoverCapsuleButton.completedMax)
            {
                brokeCapsuleDoorActivity.SetActive(false);
                recoverCapsuleButtonActivity.SetActive(false);
                ExitFromCapsuleActivity.SetActive(true);
            }
            else
            {
                ExitFromCapsuleActivity.SetActive(false);
            }

            if (RepeatInvestigateCapsule.completed == RepeatInvestigateCapsule.completedMax)
            {
                repeatInvestigateCapsuleActivity.SetActive(false);
                O2MaskActivity.SetActive(true);
            }

            if (O2Mask.completed == O2Mask.completedMax)
            {
                TakeO2MaskActivity.SetActive(TakeO2Mask.completed != TakeO2Mask.completedMax);
                O2MaskActivity.SetActive(false);
            }

            if (TakeO2Mask.completed == TakeO2Mask.completedMax)
            {
                TakeO2MaskActivity.SetActive(false);
            }
        }
        else if (gameManager.currentLevel == GameManager.Level.Cabin)
        {
            var InvestigateCabin = Stats.activities.FirstOrDefault(n => n.name == "InvestigateCabin");
            var InvestigateCabinActivity = Activities.FirstOrDefault(n => n.name == "InvestigateCabin");

            InvestigateCabinActivity.SetActive(InvestigateCabin.completed != InvestigateCabin.completedMax);
        }
    }
}
