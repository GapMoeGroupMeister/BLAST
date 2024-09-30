using DG.Tweening;

public class GameUICanvas : UIPanel
{

    public override void Close()
    {
        transform.DOScaleY(1.3f, _activeDuration);
        SetCanvasActiveImmediately(false);
    }

    public override void Open()
    {
        transform.DOScaleY(1f, _activeDuration);
        SetCanvasActiveImmediately(true);
    }

}
