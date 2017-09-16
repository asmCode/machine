using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private BoardManager mBoardManager;
    private BoardViewController mBoardViewController;
    private BoardView mBoardView;

    public void Init()
    {
        ElementFactory.RegisterElementCreator((int)ElementType.ConstantSignalSource, () => { return new ConstantSignalSource(); });
        ElementFactory.RegisterElementCreator((int)ElementType.GateAnd, () => { return new GateAnd(); });
        ElementFactory.RegisterElementCreator((int)ElementType.GateNot, () => { return new GateNot(); });

        mBoardManager = new BoardManager();
        mBoardView = new BoardView();
        mBoardViewController = new BoardViewController(mBoardManager, mBoardView);

        int id1 = mBoardManager.AddElement((int)ElementType.ConstantSignalSource, new Point3(0, 0, -1));
        int id2 = mBoardManager.AddElement((int)ElementType.GateAnd, new Point3(-1, 0, 0));
        int id3 = mBoardManager.AddElement((int)ElementType.ConstantSignalSource, new Point3(0, 0, 1));

        int not1Id = mBoardManager.AddElement((int)ElementType.GateNot, new Point3(-2, 0, 0));
        int not2Id = mBoardManager.AddElement((int)ElementType.GateNot, new Point3(-3, 0, 0));

        var source = mBoardManager.GetElementById(id1);
        var source2 = mBoardManager.GetElementById(id3);
        var and = mBoardManager.GetElementById(id2);
        var not1 = mBoardManager.GetElementById(not1Id);
        var not2 = mBoardManager.GetElementById(not2Id);

        mBoardManager.SetConnection(and.InputPinIds[0], source.OutputPinIds[0]);
        mBoardManager.SetConnection(and.InputPinIds[1], source2.OutputPinIds[0]);

        mBoardManager.SetConnection(not2.InputPinIds[0], not1.OutputPinIds[0]);
        mBoardManager.SetConnection(not1.InputPinIds[0], and.OutputPinIds[0]);

        mBoardManager.ResolveAll();
        mBoardManager.Dump();

        mBoardViewController.Update();
    }
}
