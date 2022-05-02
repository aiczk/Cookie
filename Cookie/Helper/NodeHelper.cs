using FFXIVClientStructs.FFXIV.Client.System.Memory;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace Cookie.Helper;

public static unsafe class NodeHelper
{
    private const uint NodeId = 50000;

    public static AtkImageNode* CreateImageNode(int iconId)
    {
        var imageNode = IMemorySpace.GetUISpace()->Create<AtkImageNode>();
        imageNode->AtkResNode.Type = NodeType.Image;
        imageNode->AtkResNode.Flags = (short)(NodeFlags.AnchorLeft | NodeFlags.AnchorTop);
        imageNode->AtkResNode.DrawFlags = 0;
        imageNode->AtkResNode.Width = 32;
        imageNode->AtkResNode.Height = 32;
        imageNode->AtkResNode.ToggleVisibility(true);
        imageNode->WrapMode = 2;
        imageNode->Flags |= (byte)ImageNodeFlags.AutoFit;

        var asset = Malloc<AtkUldAsset>();
        var part = Malloc<AtkUldPart>();
        var partsList = Malloc<AtkUldPartsList>();

        asset->Id = 0;
        asset->AtkTexture.Ctor();
        
        part->U = 0;
        part->V = 0;
        part->Width = 32;
        part->Height = 32;
        part->UldAsset = asset;
        
        partsList->Id = 0;
        partsList->PartCount = 1;
        partsList->Parts = part;

        imageNode->PartsList = partsList;
        imageNode->LoadIconTexture(iconId, 0);

        return imageNode;
    }

    public static AtkResNode* CreateRootNode()
    {
        var resNode = IMemorySpace.GetUISpace()->Create<AtkResNode>();
        resNode->Type = NodeType.Res;
        resNode->Flags = (short)(NodeFlags.AnchorLeft | NodeFlags.AnchorTop);
        resNode->NodeID = NodeId;
        resNode->ToggleVisibility(true);
        return resNode;
    }

    public static AtkResNode* Build(AtkResNode* rootNode)
    {
        var root = CreateRootNode();
        var image = (AtkResNode*) CreateImageNode(0);

        root->NodeID = NodeId;
        root->ChildCount = 1;
        root->ChildNode = image;

        image->NodeID = root->NodeID + 1;
        image->ParentNode = root;
        image->PrevSiblingNode = null;
        image->ToggleVisibility(false);
        
        AddNode(rootNode, root);

        return root;
    }

    public static void AddNode(AtkResNode* rootNode, AtkResNode* node)
    {
        var lastNode = rootNode->ChildNode;
        while (lastNode->PrevSiblingNode != null)
            lastNode = lastNode->PrevSiblingNode;

        lastNode->PrevSiblingNode = node;

        node->ParentNode = lastNode->ParentNode;
        node->NextSiblingNode = lastNode;
        node->NodeID = lastNode->NodeID + 1;

        rootNode->ChildCount += 1;
    }

    public static void Sdf(AtkResNode* rootNode,AtkResNode* node)
    {
        var lastNode = rootNode;
        while (lastNode->PrevSiblingNode != null)
            lastNode = lastNode->PrevSiblingNode;

        lastNode->PrevSiblingNode = node;
        node->NextSiblingNode = lastNode;
    }

    private static T* Malloc<T>() where T : unmanaged => (T*)IMemorySpace.GetUISpace()->Malloc<T>();
}