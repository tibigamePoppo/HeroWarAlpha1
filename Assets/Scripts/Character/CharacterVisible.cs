using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Fusion;

namespace Unit
{
    public class CharacterVisible : NetworkBehaviour
    {
        [SerializeField]
        private GameObject[] VisibleObjects;
        private CharacterStatus MyStatus;
        void Start()
        {
            RPC_ChangeVisible(false);

            MyStatus = GetComponent<CharacterStatus>();
            MyStatus
                .OniVisibleChanged
                .Subscribe(value =>
                {
                    RPC_ChangeVisible(value);
                }
            );
        }
        /// <summary>
        /// 視覚化の切り替え
        /// </summary>
        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        private void RPC_ChangeVisible(bool value)
        {   
            if(!HasInputAuthority)
            {
                foreach (var Objects in VisibleObjects)
                {
                    Objects.SetActive(value);
                }
            }
        } 
    }
}