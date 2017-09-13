using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private BoardManager mBoardManager;

    public void Init()
    {
        ElementFactory.RegisterElementCreator((int)ElementType.ConstantSignalSource, () => { return new ConstantSignalSource(); });
        ElementFactory.RegisterElementCreator((int)ElementType.GateAnd, () => { return new GateAnd(); });
        ElementFactory.RegisterElementCreator((int)ElementType.GateNot, () => { return new GateNot(); });

        mBoardManager = new BoardManager();

        int id1 = mBoardManager.AddElement((int)ElementType.ConstantSignalSource);
        int id2 = mBoardManager.AddElement((int)ElementType.GateAnd);
        int id3 = mBoardManager.AddElement((int)ElementType.ConstantSignalSource);

        int not1Id = mBoardManager.AddElement((int)ElementType.GateNot);
        int not2Id = mBoardManager.AddElement((int)ElementType.GateNot);

        var source = mBoardManager.GetElement(id1);
        var source2 = mBoardManager.GetElement(id3);
        var and = mBoardManager.GetElement(id2);
        var not1 = mBoardManager.GetElement(not1Id);
        var not2 = mBoardManager.GetElement(not2Id);

        mBoardManager.SetConnection(and.InputPinIds[0], source.OutputPinIds[0]);
        mBoardManager.SetConnection(and.InputPinIds[1], source2.OutputPinIds[0]);

        mBoardManager.SetConnection(not2.InputPinIds[0], not1.OutputPinIds[0]);
        mBoardManager.SetConnection(not1.InputPinIds[0], and.OutputPinIds[0]);

        mBoardManager.ResolveAll();
        mBoardManager.Dump();
    }
}
