import{_ as f,b as v,A as c,r as V,w as b,k as K,l as C,P as x,d as i,V as k,K as w,e as u,o as a,c as n,a as t,t as d,F as m,f as p,g as I,h,p as S,i as A}from"./index-d52ca69b.js";const M={setup(){v(()=>{_(),c.myVaults});const s=V({});b(()=>{s.value={...c.account}});async function _(){try{const r=c.account.id;await K.getCreatorKeeps(r),C.log("account page Keep GET")}catch(r){x.error(r)}}return{editable:s,account:i(()=>c.account),myVaults:i(()=>c.myVaults),myKeeps:i(()=>c.myKeeps)}},components:{VaultCard:k,KeepCard:w}},l=s=>(S("data-v-c5a9b802"),s=s(),A(),s),P={key:0,class:"container-fluid"},B={class:"row"},E={class:"about text-center mt-3 px-0"},N=["src"],F={class:"welcome font-dancing"},D=["src"],G=l(()=>t("div",{class:"text-center mt-5"},null,-1)),L={class:"text-center mt-5"},O={class:"mt-md-5 mt-2"},T={class:"mt-2"},W=l(()=>t("hr",{class:"mt-3"},null,-1)),j={class:"row"},q=l(()=>t("button",{class:"btn theme-btn theme-font col-md-2 col-6 ms-2","data-bs-toggle":"modal","data-bs-target":"#create-vault"}," Create a Vault ",-1)),z=l(()=>t("p",null,"My Vaults",-1)),H=l(()=>t("p",null,"My Keeps",-1)),J={class:"my-2 masonry-layout"};function Q(s,_,r,e,R,U){const y=u("VaultCard"),g=u("KeepCard");return e.account.id?(a(),n("section",P,[t("section",B,[t("div",E,[t("img",{src:e.account.coverImg,alt:"",class:"account-cover position-absolute"},null,8,N),t("div",F,"Welcome, "+d(e.account.name.slice(0,e.account.name.indexOf("@"))),1),t("img",{class:"rounded-circle profile-pic mb-5",src:e.account.picture,alt:""},null,8,D),G]),t("div",L,[t("p",O,d(e.myVaults.length)+"Vaults||"+d(e.myKeeps.length)+"Keeps",1),t("p",T,d(e.account.email),1)]),W,t("section",j,[q,z,(a(!0),n(m,null,p(e.myVaults,o=>(a(),n("div",{key:o.id,class:"col-md-3 col-6 g-3"},[h(y,{vault:o},null,8,["vault"])]))),128))]),H,t("section",J,[(a(!0),n(m,null,p(e.myKeeps,o=>(a(),n("div",{key:o.id,class:""},[h(g,{keep:o},null,8,["keep"])]))),128))])])])):I("",!0)}const Y=f(M,[["render",Q],["__scopeId","data-v-c5a9b802"]]);export{Y as default};
