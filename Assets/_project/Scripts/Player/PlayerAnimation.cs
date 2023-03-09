using UnityEngine;

public class PlayerAnimation : PlayerComponent
{
    [SerializeField] private Animator _animator; // —сылка на компонент аниматора персонажа

    private static int IS_WALKING_HASH = Animator.StringToHash("isWalking");
    private static int HARVEST_HASH = Animator.StringToHash("isHarvesting");

    public void SetIsWalking(bool isWalking)
    {
        _animator.SetBool(IS_WALKING_HASH, isWalking);
    }

    public void SetHarvesting(bool isHarvesting)
    {
        _animator.SetBool(HARVEST_HASH, isHarvesting);
    }

    public void CanHarvest()
    {
        Controller.Harvest.CanUseTool(true);
    }

    public void EndHarvest()
    {
        Controller.Harvest.CanUseTool(false);
    }
}
