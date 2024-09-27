import{d as be,n as dt,x as Pe,bo as ft,bn as vt,bl as me,bp as mt,aU as ht,y as W,ac as X,ae as oe,aG as gt,aj as Se,bq as Me,B as Q,an as yt,ap as Le,br as He,T as L,c as S,ar as fe,bs as bt,L as se,Y as te,G as le,g as r,bt as Te,O as J,bu as pt,aE as Ge,ak as Ve,al as Ue,ao as $e,aq as Ne,at as E,I as q,aK as Ae,ad as kt,af as qe,ag as St,bv as Vt,ah as Ct,bw as xt,ai as It,bx as Ke,am as We,by as wt,bz as Pt,bA as je,bB as Tt,bC as At,bD as _t,C as ae,bE as Bt,bF as Ft,_ as ve,$ as Je,bG as Rt,bH as Dt,M as ue,a7 as Xe,F as Y,bI as pe,K as ee,r as zt,e as he,f as _e,t as Be,v as Et,q as Ot,j as Mt,p as Lt,h as Ht,w as Gt,l as Ye,D as Qe,ax as Ut,bJ as Ce,H as $t,ay as Nt,aD as de,bK as qt,aI as Kt,bL as ke,a2 as xe,z as Ze,W as Fe,a5 as ge,bM as Wt,bN as jt,az as Jt,bO as Xt,bP as Yt,a4 as ce,X as Qt,bQ as Zt,bR as el,bS as tl,bT as ll,bU as nl,bV as al,bW as ol,bX as Re,bY as il,a1 as ul}from"./index-c3e40adb.js";import{e as sl,g as rl,h as cl,V as De}from"./VTextField-e1b68920.js";class et{constructor(n,t,a,o,m,v,u,l,k,P,V=""){n?(this.department=be(n),n.hasOwnProperty("id")?this.departmentId=n.id:n.hasOwnProperty("value")?this.departmentId=n.value:this.departmentId=0):(this.department=null,this.departmentId=null),t?(this.type=be(t),this.typeId=t.id):(this.type=null,this.typeId=0),this.judgeDate=a,this.judgeDateModel=et.iniJudgeDateModel(a),this.originType=m,this.courtType=v,this.fileNumber=o,this.courtType=v,this.year=u,this.category=l,this.num=k,this.file=P,this.ps=V,this.errors=new dt}static iniJudgeDateModel(n){let t=Pe(n);if(t){const a=ft(t);return{date:vt(n),value:a,model:{text:t,text_cn:a,num:n},error_message:""}}return{date:null,value:"",model:{text:"",text_cn:"",num:0},error_message:""}}static checkJudgeDate(n){return!!Pe(n)}static checkType(n){return!!(n.id&&n.key)}static checkFileNumber(n){return n.toString(),!0}static checkYear(n){let t=n.toString();return me(t)&&t.length<4&&t.length>1}static isValidNum(n){let t=n.toString();return me(t)&&t.length<=6}static checkNum(n){if(!n)return"";let t=n.toString();return me(t)&&t.length<=6?mt(ht(n),6):""}}function dl(e){let{selectedElement:n,containerElement:t,isRtl:a,isHorizontal:o}=e;const m=re(o,t),v=tt(o,a,t),u=re(o,n),l=lt(o,n),k=u*.4;return v>l?l-k:v+m<l+u?l-m+u+k:v}function fl(e){let{selectedElement:n,containerElement:t,isHorizontal:a}=e;const o=re(a,t),m=lt(a,n),v=re(a,n);return m-o/2+v/2}function ze(e,n){const t=e?"scrollWidth":"scrollHeight";return(n==null?void 0:n[t])||0}function vl(e,n){const t=e?"clientWidth":"clientHeight";return(n==null?void 0:n[t])||0}function tt(e,n,t){if(!t)return 0;const{scrollLeft:a,offsetWidth:o,scrollWidth:m}=t;return e?n?m-o+a:a:t.scrollTop}function re(e,n){const t=e?"offsetWidth":"offsetHeight";return(n==null?void 0:n[t])||0}function lt(e,n){const t=e?"offsetLeft":"offsetTop";return(n==null?void 0:n[t])||0}const ml=Symbol.for("vuetify:v-slide-group"),nt=W({centerActive:Boolean,direction:{type:String,default:"horizontal"},symbol:{type:null,default:ml},nextIcon:{type:X,default:"$next"},prevIcon:{type:X,default:"$prev"},showArrows:{type:[Boolean,String],validator:e=>typeof e=="boolean"||["always","desktop","mobile"].includes(e)},...oe(),...gt({mobile:null}),...Se(),...Me({selectedClass:"v-slide-group-item--active"})},"VSlideGroup"),Ee=Q()({name:"VSlideGroup",props:nt(),emits:{"update:modelValue":e=>!0},setup(e,n){let{slots:t}=n;const{isRtl:a}=yt(),{displayClasses:o,mobile:m}=Le(e),v=He(e,e.symbol),u=L(!1),l=L(0),k=L(0),P=L(0),V=S(()=>e.direction==="horizontal"),{resizeRef:h,contentRect:x}=fe(),{resizeRef:y,contentRect:w}=fe(),s=bt(),I=S(()=>({container:h.el,duration:200,easing:"easeOutQuart"})),D=S(()=>v.selected.value.length?v.items.value.findIndex(i=>i.id===v.selected.value[0]):-1),z=S(()=>v.selected.value.length?v.items.value.findIndex(i=>i.id===v.selected.value[v.selected.value.length-1]):-1);if(se){let i=-1;te(()=>[v.selected.value,x.value,w.value,V.value],()=>{cancelAnimationFrame(i),i=requestAnimationFrame(()=>{if(x.value&&w.value){const f=V.value?"width":"height";k.value=x.value[f],P.value=w.value[f],u.value=k.value+1<P.value}if(D.value>=0&&y.el){const f=y.el.children[z.value];H(f,e.centerActive)}})})}const B=L(!1);function H(i,f){let b=0;f?b=fl({containerElement:h.el,isHorizontal:V.value,selectedElement:i}):b=dl({containerElement:h.el,isHorizontal:V.value,isRtl:a.value,selectedElement:i}),p(b)}function p(i){if(!se||!h.el)return;const f=re(V.value,h.el),b=tt(V.value,a.value,h.el);if(!(ze(V.value,h.el)<=f||Math.abs(i-b)<16)){if(V.value&&a.value&&h.el){const{scrollWidth:F,offsetWidth:A}=h.el;i=F-A-i}V.value?s.horizontal(i,I.value):s(i,I.value)}}function d(i){const{scrollTop:f,scrollLeft:b}=i.target;l.value=V.value?b:f}function T(i){if(B.value=!0,!(!u.value||!y.el)){for(const f of i.composedPath())for(const b of y.el.children)if(b===f){H(b);return}}}function _(i){B.value=!1}let R=!1;function O(i){var f;!R&&!B.value&&!(i.relatedTarget&&((f=y.el)!=null&&f.contains(i.relatedTarget)))&&$(),R=!1}function K(){R=!0}function j(i){if(!y.el)return;function f(b){i.preventDefault(),$(b)}V.value?i.key==="ArrowRight"?f(a.value?"prev":"next"):i.key==="ArrowLeft"&&f(a.value?"next":"prev"):i.key==="ArrowDown"?f("next"):i.key==="ArrowUp"&&f("prev"),i.key==="Home"?f("first"):i.key==="End"&&f("last")}function $(i){var b,C;if(!y.el)return;let f;if(!i)f=pt(y.el)[0];else if(i==="next"){if(f=(b=y.el.querySelector(":focus"))==null?void 0:b.nextElementSibling,!f)return $("first")}else if(i==="prev"){if(f=(C=y.el.querySelector(":focus"))==null?void 0:C.previousElementSibling,!f)return $("last")}else i==="first"?f=y.el.firstElementChild:i==="last"&&(f=y.el.lastElementChild);f&&f.focus({preventScroll:!0})}function M(i){const f=V.value&&a.value?-1:1,b=(i==="prev"?-f:f)*k.value;let C=l.value+b;if(V.value&&a.value&&h.el){const{scrollWidth:F,offsetWidth:A}=h.el;C+=F-A}p(C)}const ne=S(()=>({next:v.next,prev:v.prev,select:v.select,isSelected:v.isSelected})),Z=S(()=>{switch(e.showArrows){case"always":return!0;case"desktop":return!m.value;case!0:return u.value||Math.abs(l.value)>0;case"mobile":return m.value||u.value||Math.abs(l.value)>0;default:return!m.value&&(u.value||Math.abs(l.value)>0)}}),g=S(()=>Math.abs(l.value)>1),c=S(()=>{if(!h.value)return!1;const i=ze(V.value,h.el),f=vl(V.value,h.el);return i-f-Math.abs(l.value)>1});return le(()=>r(e.tag,{class:["v-slide-group",{"v-slide-group--vertical":!V.value,"v-slide-group--has-affixes":Z.value,"v-slide-group--is-overflowing":u.value},o.value,e.class],style:e.style,tabindex:B.value||v.selected.value.length?-1:0,onFocus:O},{default:()=>{var i,f,b;return[Z.value&&r("div",{key:"prev",class:["v-slide-group__prev",{"v-slide-group__prev--disabled":!g.value}],onMousedown:K,onClick:()=>g.value&&M("prev")},[((i=t.prev)==null?void 0:i.call(t,ne.value))??r(Te,null,{default:()=>[r(J,{icon:a.value?e.nextIcon:e.prevIcon},null)]})]),r("div",{key:"container",ref:h,class:"v-slide-group__container",onScroll:d},[r("div",{ref:y,class:"v-slide-group__content",onFocusin:T,onFocusout:_,onKeydown:j},[(f=t.default)==null?void 0:f.call(t,ne.value)])]),Z.value&&r("div",{key:"next",class:["v-slide-group__next",{"v-slide-group__next--disabled":!c.value}],onMousedown:K,onClick:()=>c.value&&M("next")},[((b=t.next)==null?void 0:b.call(t,ne.value))??r(Te,null,{default:()=>[r(J,{icon:a.value?e.prevIcon:e.nextIcon},null)]})])]}})),{selected:v.selected,scrollTo:M,scrollOffset:l,focus:$}}}),at=Symbol.for("vuetify:v-chip-group"),hl=W({column:Boolean,filter:Boolean,valueComparator:{type:Function,default:Ge},...nt(),...oe(),...Me({selectedClass:"v-chip--selected"}),...Se(),...Ve(),...Ue({variant:"tonal"})},"VChipGroup");Q()({name:"VChipGroup",props:hl(),emits:{"update:modelValue":e=>!0},setup(e,n){let{slots:t}=n;const{themeClasses:a}=$e(e),{isSelected:o,select:m,next:v,prev:u,selected:l}=He(e,at);return Ne({VChip:{color:E(e,"color"),disabled:E(e,"disabled"),filter:E(e,"filter"),variant:E(e,"variant")}}),le(()=>{const k=Ee.filterProps(e);return r(Ee,q(k,{class:["v-chip-group",{"v-chip-group--column":e.column},a.value,e.class],style:e.style}),{default:()=>{var P;return[(P=t.default)==null?void 0:P.call(t,{isSelected:o,select:m,next:v,prev:u,selected:l.value})]}})}),{}}});const gl=W({activeClass:String,appendAvatar:String,appendIcon:X,closable:Boolean,closeIcon:{type:X,default:"$delete"},closeLabel:{type:String,default:"$vuetify.close"},draggable:Boolean,filter:Boolean,filterIcon:{type:String,default:"$complete"},label:Boolean,link:{type:Boolean,default:void 0},pill:Boolean,prependAvatar:String,prependIcon:X,ripple:{type:[Boolean,Object],default:!0},text:String,modelValue:{type:Boolean,default:!0},onClick:Ae(),onClickOnce:Ae(),...kt(),...oe(),...qe(),...St(),...Vt(),...Ct(),...xt(),...It(),...Se({tag:"span"}),...Ve(),...Ue({variant:"tonal"})},"VChip"),ot=Q()({name:"VChip",directives:{Ripple:Ke},props:gl(),emits:{"click:close":e=>!0,"update:modelValue":e=>!0,"group:selected":e=>!0,click:e=>!0},setup(e,n){let{attrs:t,emit:a,slots:o}=n;const{t:m}=We(),{borderClasses:v}=wt(e),{colorClasses:u,colorStyles:l,variantClasses:k}=Pt(e),{densityClasses:P}=je(e),{elevationClasses:V}=Tt(e),{roundedClasses:h}=At(e),{sizeClasses:x}=_t(e),{themeClasses:y}=$e(e),w=ae(e,"modelValue"),s=Bt(e,at,!1),I=Ft(e,t),D=S(()=>e.link!==!1&&I.isLink.value),z=S(()=>!e.disabled&&e.link!==!1&&(!!s||e.link||I.isClickable.value)),B=S(()=>({"aria-label":m(e.closeLabel),onClick(d){d.preventDefault(),d.stopPropagation(),w.value=!1,a("click:close",d)}}));function H(d){var T;a("click",d),z.value&&((T=I.navigate)==null||T.call(I,d),s==null||s.toggle())}function p(d){(d.key==="Enter"||d.key===" ")&&(d.preventDefault(),H(d))}return()=>{const d=I.isLink.value?"a":e.tag,T=!!(e.appendIcon||e.appendAvatar),_=!!(T||o.append),R=!!(o.close||e.closable),O=!!(o.filter||e.filter)&&s,K=!!(e.prependIcon||e.prependAvatar),j=!!(K||o.prepend),$=!s||s.isSelected.value;return w.value&&ve(r(d,{class:["v-chip",{"v-chip--disabled":e.disabled,"v-chip--label":e.label,"v-chip--link":z.value,"v-chip--filter":O,"v-chip--pill":e.pill},y.value,v.value,$?u.value:void 0,P.value,V.value,h.value,x.value,k.value,s==null?void 0:s.selectedClass.value,e.class],style:[$?l.value:void 0,e.style],disabled:e.disabled||void 0,draggable:e.draggable,href:I.href.value,tabindex:z.value?0:void 0,onClick:H,onKeydown:z.value&&!D.value&&p},{default:()=>{var M;return[Rt(z.value,"v-chip"),O&&r(Dt,{key:"filter"},{default:()=>[ve(r("div",{class:"v-chip__filter"},[o.filter?r(ue,{key:"filter-defaults",disabled:!e.filterIcon,defaults:{VIcon:{icon:e.filterIcon}}},o.filter):r(J,{key:"filter-icon",icon:e.filterIcon},null)]),[[Xe,s.isSelected.value]])]}),j&&r("div",{key:"prepend",class:"v-chip__prepend"},[o.prepend?r(ue,{key:"prepend-defaults",disabled:!K,defaults:{VAvatar:{image:e.prependAvatar,start:!0},VIcon:{icon:e.prependIcon,start:!0}}},o.prepend):r(Y,null,[e.prependIcon&&r(J,{key:"prepend-icon",icon:e.prependIcon,start:!0},null),e.prependAvatar&&r(pe,{key:"prepend-avatar",image:e.prependAvatar,start:!0},null)])]),r("div",{class:"v-chip__content","data-no-activator":""},[((M=o.default)==null?void 0:M.call(o,{isSelected:s==null?void 0:s.isSelected.value,selectedClass:s==null?void 0:s.selectedClass.value,select:s==null?void 0:s.select,toggle:s==null?void 0:s.toggle,value:s==null?void 0:s.value.value,disabled:e.disabled}))??e.text]),_&&r("div",{key:"append",class:"v-chip__append"},[o.append?r(ue,{key:"append-defaults",disabled:!T,defaults:{VAvatar:{end:!0,image:e.appendAvatar},VIcon:{end:!0,icon:e.appendIcon}}},o.append):r(Y,null,[e.appendIcon&&r(J,{key:"append-icon",end:!0,icon:e.appendIcon},null),e.appendAvatar&&r(pe,{key:"append-avatar",end:!0,image:e.appendAvatar},null)])]),R&&r("button",q({key:"close",class:"v-chip__close",type:"button"},B.value),[o.close?r(ue,{key:"close-defaults",defaults:{VIcon:{icon:e.closeIcon,size:"x-small"}}},o.close):r(J,{key:"close-icon",icon:e.closeIcon,size:"x-small"},null)])]}}),[[Je("ripple"),z.value&&e.ripple,null]])}}}),yl=["multiple","accept"],El={__name:"Upload",props:{show_button:{type:Boolean,default:!1},button_label:{type:String,default:"上傳檔案"},multiple:{type:Boolean,default:!0},is_media:{type:Boolean,default:!0},allow_types:{type:Array,default:()=>[]},exclude:{type:Array,default:()=>[]}},emits:["file-added","file-removed"],setup(e,{expose:n,emit:t}){const a=e;n({launch:h,getFiles:V});const o=t,m=ee(null),v=["image/png","image/gif","image/jpeg"],l=zt(be({files:[],thumbnails:[]})),k=S(()=>a.is_media?v.toString():a.allow_types.toString());S(()=>l.files.map(p=>p.name));const P=S(()=>!!l.files.length);function V(){return l.files}function h(){m.value.click()}function x(p){let d=p.target.files||p.dataTransfer.files;if(!d.length)return;let T=[];for(let _=0;_<d.length;_++)y(d[_])&&T.push(I(d[_]));Promise.all(T).then(()=>{a.is_media?o("file-added",{files:l.files,thumbs:l.thumbnails}):o("file-added",l.files)})}function y(p){return!(w(p.name)||a.exclude.includes(p.name))}function w(p){return s(p)>=0}function s(p){return l.files.findIndex(d=>d.name===p)}function I(p){return a.is_media?new Promise((d,T)=>{B(p).then(R=>{let O={data:R,name:p.name,type:p.type};l.files.push(p),l.thumbnails.push(O),d(!0)}).catch(R=>{console.error(R),T()})}):new Promise(d=>{l.files.push(p),d(!0)})}function D(p){let d=s(p);d>=0&&(l.files.splice(d,1),l.files.length||(m.value.value="")),z(p),o("file-removed",l.files)}function z(p){let d=l.thumbnails.findIndex(T=>T.name==p);d>=0&&l.thumbnails.splice(d,1)}function B(p){return new Promise((d,T)=>{H(p.type)||d(null);const _=new FileReader;_.onerror=R=>{_.abort(),T(R)},_.onload=()=>{d(_.result)},_.readAsDataURL(p)})}function H(p){return v.includes(p)}return(p,d)=>(he(),_e("div",null,[ve(r(Ot,q({variant:"outlined"},a,{color:"primary",textContent:Be(e.button_label),disabled:P.value,onClick:Et(h,["prevent"])}),null,16,["textContent","disabled"]),[[Xe,e.show_button]]),Mt("input",{ref_key:"inputUpload",ref:m,style:{display:"none"},type:"file",multiple:e.multiple,accept:k.value,onChange:x},null,40,yl),(he(!0),_e(Y,null,Lt(l.files,T=>(he(),Ht(ot,{size:"small",class:"ma-2",closable:"",key:T.name,"onClick:close":_=>D(T.name)},{default:Gt(()=>[Ye(Be(T.name),1)]),_:2},1032,["onClick:close"]))),128))]))}};const it=Symbol.for("vuetify:selection-control-group"),ut=W({color:String,disabled:{type:Boolean,default:null},defaultsTarget:String,error:Boolean,id:String,inline:Boolean,falseIcon:X,trueIcon:X,ripple:{type:[Boolean,Object],default:!0},multiple:{type:Boolean,default:null},name:String,readonly:{type:Boolean,default:null},modelValue:null,type:String,valueComparator:{type:Function,default:Ge},...oe(),...qe(),...Ve()},"SelectionControlGroup"),bl=W({...ut({defaultsTarget:"VSelectionControl"})},"VSelectionControlGroup");Q()({name:"VSelectionControlGroup",props:bl(),emits:{"update:modelValue":e=>!0},setup(e,n){let{slots:t}=n;const a=ae(e,"modelValue"),o=Qe(),m=S(()=>e.id||`v-selection-control-group-${o}`),v=S(()=>e.name||m.value),u=new Set;return Ut(it,{modelValue:a,forceUpdate:()=>{u.forEach(l=>l())},onForceUpdate:l=>{u.add(l),Ce(()=>{u.delete(l)})}}),Ne({[e.defaultsTarget]:{color:E(e,"color"),disabled:E(e,"disabled"),density:E(e,"density"),error:E(e,"error"),inline:E(e,"inline"),modelValue:a,multiple:S(()=>!!e.multiple||e.multiple==null&&Array.isArray(a.value)),name:v,falseIcon:E(e,"falseIcon"),trueIcon:E(e,"trueIcon"),readonly:E(e,"readonly"),ripple:E(e,"ripple"),type:E(e,"type"),valueComparator:E(e,"valueComparator")}}),le(()=>{var l;return r("div",{class:["v-selection-control-group",{"v-selection-control-group--inline":e.inline},e.class],style:e.style,role:e.type==="radio"?"radiogroup":void 0},[(l=t.default)==null?void 0:l.call(t)])}),{}}});const st=W({label:String,baseColor:String,trueValue:null,falseValue:null,value:null,...oe(),...ut()},"VSelectionControl");function pl(e){const n=Nt(it,void 0),{densityClasses:t}=je(e),a=ae(e,"modelValue"),o=S(()=>e.trueValue!==void 0?e.trueValue:e.value!==void 0?e.value:!0),m=S(()=>e.falseValue!==void 0?e.falseValue:!1),v=S(()=>!!e.multiple||e.multiple==null&&Array.isArray(a.value)),u=S({get(){const x=n?n.modelValue.value:a.value;return v.value?de(x).some(y=>e.valueComparator(y,o.value)):e.valueComparator(x,o.value)},set(x){if(e.readonly)return;const y=x?o.value:m.value;let w=y;v.value&&(w=x?[...de(a.value),y]:de(a.value).filter(s=>!e.valueComparator(s,o.value))),n?n.modelValue.value=w:a.value=w}}),{textColorClasses:l,textColorStyles:k}=qt(S(()=>{if(!(e.error||e.disabled))return u.value?e.color:e.baseColor})),{backgroundColorClasses:P,backgroundColorStyles:V}=Kt(S(()=>u.value&&!e.error&&!e.disabled?e.color:e.baseColor)),h=S(()=>u.value?e.trueIcon:e.falseIcon);return{group:n,densityClasses:t,trueValue:o,falseValue:m,model:u,textColorClasses:l,textColorStyles:k,backgroundColorClasses:P,backgroundColorStyles:V,icon:h}}const Oe=Q()({name:"VSelectionControl",directives:{Ripple:Ke},inheritAttrs:!1,props:st(),emits:{"update:modelValue":e=>!0},setup(e,n){let{attrs:t,slots:a}=n;const{group:o,densityClasses:m,icon:v,model:u,textColorClasses:l,textColorStyles:k,backgroundColorClasses:P,backgroundColorStyles:V,trueValue:h}=pl(e),x=Qe(),y=L(!1),w=L(!1),s=ee(),I=S(()=>e.id||`input-${x}`),D=S(()=>!e.disabled&&!e.readonly);o==null||o.onForceUpdate(()=>{s.value&&(s.value.checked=u.value)});function z(d){D.value&&(y.value=!0,ke(d.target,":focus-visible")!==!1&&(w.value=!0))}function B(){y.value=!1,w.value=!1}function H(d){d.stopPropagation()}function p(d){if(!D.value){s.value&&(s.value.checked=u.value);return}e.readonly&&o&&xe(()=>o.forceUpdate()),u.value=d.target.checked}return le(()=>{var O,K;const d=a.label?a.label({label:e.label,props:{for:I.value}}):e.label,[T,_]=$t(t),R=r("input",q({ref:s,checked:u.value,disabled:!!e.disabled,id:I.value,onBlur:B,onFocus:z,onInput:p,"aria-disabled":!!e.disabled,"aria-label":e.label,type:e.type,value:h.value,name:e.name,"aria-checked":e.type==="checkbox"?u.value:void 0},_),null);return r("div",q({class:["v-selection-control",{"v-selection-control--dirty":u.value,"v-selection-control--disabled":e.disabled,"v-selection-control--error":e.error,"v-selection-control--focused":y.value,"v-selection-control--focus-visible":w.value,"v-selection-control--inline":e.inline},m.value,e.class]},T,{style:e.style}),[r("div",{class:["v-selection-control__wrapper",l.value],style:k.value},[(O=a.default)==null?void 0:O.call(a,{backgroundColorClasses:P,backgroundColorStyles:V}),ve(r("div",{class:["v-selection-control__input"]},[((K=a.input)==null?void 0:K.call(a,{model:u,textColorClasses:l,textColorStyles:k,backgroundColorClasses:P,backgroundColorStyles:V,inputNode:R,icon:v.value,props:{onFocus:z,onBlur:B,id:I.value}}))??r(Y,null,[v.value&&r(J,{key:"icon",icon:v.value},null),R])]),[[Je("ripple"),e.ripple&&[!e.disabled&&!e.readonly,null,["center","circle"]]]])]),d&&r(sl,{for:I.value,onClick:H},{default:()=>[d]})])}),{isFocused:y,input:s}}}),kl=W({indeterminate:Boolean,indeterminateIcon:{type:X,default:"$checkboxIndeterminate"},...st({falseIcon:"$checkboxOff",trueIcon:"$checkboxOn"})},"VCheckboxBtn"),Sl=Q()({name:"VCheckboxBtn",props:kl(),emits:{"update:modelValue":e=>!0,"update:indeterminate":e=>!0},setup(e,n){let{slots:t}=n;const a=ae(e,"indeterminate"),o=ae(e,"modelValue");function m(l){a.value&&(a.value=!1)}const v=S(()=>a.value?e.indeterminateIcon:e.falseIcon),u=S(()=>a.value?e.indeterminateIcon:e.trueIcon);return le(()=>{const l=Ze(Oe.filterProps(e),["modelValue"]);return r(Oe,q(l,{modelValue:o.value,"onUpdate:modelValue":[k=>o.value=k,m],class:["v-checkbox-btn",e.class],style:e.style,type:"checkbox",falseIcon:v.value,trueIcon:u.value,"aria-checked":a.value?"mixed":void 0}),t)}),{}}});const Vl=W({renderless:Boolean,...oe()},"VVirtualScrollItem"),Cl=Q()({name:"VVirtualScrollItem",inheritAttrs:!1,props:Vl(),emits:{"update:height":e=>!0},setup(e,n){let{attrs:t,emit:a,slots:o}=n;const{resizeRef:m,contentRect:v}=fe(void 0,"border");te(()=>{var u;return(u=v.value)==null?void 0:u.height},u=>{u!=null&&a("update:height",u)}),le(()=>{var u,l;return e.renderless?r(Y,null,[(u=o.default)==null?void 0:u.call(o,{itemRef:m})]):r("div",q({ref:m,class:["v-virtual-scroll__item",e.class],style:e.style},t),[(l=o.default)==null?void 0:l.call(o)])})}}),xl=-1,Il=1,ye=100,wl=W({itemHeight:{type:[Number,String],default:null},height:[Number,String]},"virtual");function Pl(e,n){const t=Le(),a=L(0);Fe(()=>{a.value=parseFloat(e.itemHeight||0)});const o=L(0),m=L(Math.ceil((parseInt(e.height)||t.height.value)/(a.value||16))||1),v=L(0),u=L(0),l=ee(),k=ee();let P=0;const{resizeRef:V,contentRect:h}=fe();Fe(()=>{V.value=l.value});const x=S(()=>{var c;return l.value===document.documentElement?t.height.value:((c=h.value)==null?void 0:c.height)||parseInt(e.height)||0}),y=S(()=>!!(l.value&&k.value&&x.value&&a.value));let w=Array.from({length:n.value.length}),s=Array.from({length:n.value.length});const I=L(0);let D=-1;function z(c){return w[c]||a.value}const B=Wt(()=>{const c=performance.now();s[0]=0;const i=n.value.length;for(let f=1;f<=i-1;f++)s[f]=(s[f-1]||0)+z(f-1);I.value=Math.max(I.value,performance.now()-c)},I),H=te(y,c=>{c&&(H(),P=k.value.offsetTop,B.immediate(),M(),~D&&xe(()=>{se&&window.requestAnimationFrame(()=>{Z(D),D=-1})}))});Ce(()=>{B.clear()});function p(c,i){const f=w[c],b=a.value;a.value=b?Math.min(a.value,i):i,(f!==i||b!==a.value)&&(w[c]=i,B())}function d(c){return c=ge(c,0,n.value.length-1),s[c]||0}function T(c){return Tl(s,c)}let _=0,R=0,O=0;te(x,(c,i)=>{i&&(M(),c<i&&requestAnimationFrame(()=>{R=0,M()}))});function K(){if(!l.value||!k.value)return;const c=l.value.scrollTop,i=performance.now();i-O>500?(R=Math.sign(c-_),P=k.value.offsetTop):R=c-_,_=c,O=i,M()}function j(){!l.value||!k.value||(R=0,O=0,M())}let $=-1;function M(){cancelAnimationFrame($),$=requestAnimationFrame(ne)}function ne(){if(!l.value||!x.value)return;const c=_-P,i=Math.sign(R),f=Math.max(0,c-ye),b=ge(T(f),0,n.value.length),C=c+x.value+ye,F=ge(T(C)+1,b+1,n.value.length);if((i!==xl||b<o.value)&&(i!==Il||F>m.value)){const A=d(o.value)-d(b),G=d(F)-d(m.value);Math.max(A,G)>ye?(o.value=b,m.value=F):(b<=0&&(o.value=b),F>=n.value.length&&(m.value=F))}v.value=d(o.value),u.value=d(n.value.length)-d(m.value)}function Z(c){const i=d(c);!l.value||c&&!i?D=c:l.value.scrollTop=i}const g=S(()=>n.value.slice(o.value,m.value).map((c,i)=>({raw:c,index:i+o.value})));return te(n,()=>{w=Array.from({length:n.value.length}),s=Array.from({length:n.value.length}),B.immediate(),M()},{deep:!0}),{containerRef:l,markerRef:k,computedItems:g,paddingTop:v,paddingBottom:u,scrollToIndex:Z,handleScroll:K,handleScrollend:j,handleItemResize:p}}function Tl(e,n){let t=e.length-1,a=0,o=0,m=null,v=-1;if(e[t]<n)return t;for(;a<=t;)if(o=a+t>>1,m=e[o],m>n)t=o-1;else if(m<n)v=o,a=o+1;else return m===n?o:a;return v}const Al=W({items:{type:Array,default:()=>[]},renderless:Boolean,...wl(),...oe(),...jt()},"VVirtualScroll"),_l=Q()({name:"VVirtualScroll",props:Al(),setup(e,n){let{slots:t}=n;const a=Jt("VVirtualScroll"),{dimensionStyles:o}=Xt(e),{containerRef:m,markerRef:v,handleScroll:u,handleScrollend:l,handleItemResize:k,scrollToIndex:P,paddingTop:V,paddingBottom:h,computedItems:x}=Pl(e,E(e,"items"));return Yt(()=>e.renderless,()=>{function y(){var I,D;const s=(arguments.length>0&&arguments[0]!==void 0?arguments[0]:!1)?"addEventListener":"removeEventListener";m.value===document.documentElement?(document[s]("scroll",u,{passive:!0}),document[s]("scrollend",l)):((I=m.value)==null||I[s]("scroll",u,{passive:!0}),(D=m.value)==null||D[s]("scrollend",l))}Qt(()=>{m.value=Zt(a.vnode.el,!0),y(!0)}),Ce(y)}),le(()=>{const y=x.value.map(w=>r(Cl,{key:w.index,renderless:e.renderless,"onUpdate:height":s=>k(w.index,s)},{default:s=>{var I;return(I=t.default)==null?void 0:I.call(t,{item:w.raw,index:w.index,...s})}}));return e.renderless?r(Y,null,[r("div",{ref:v,class:"v-virtual-scroll__spacer",style:{paddingTop:ce(V.value)}},null),y,r("div",{class:"v-virtual-scroll__spacer",style:{paddingBottom:ce(h.value)}},null)]):r("div",{ref:m,class:["v-virtual-scroll",e.class],onScrollPassive:u,onScrollend:l,style:[o.value,e.style]},[r("div",{ref:v,class:"v-virtual-scroll__container",style:{paddingTop:ce(V.value),paddingBottom:ce(h.value)}},[y])])}),{scrollToIndex:P}}});function Bl(e,n){const t=L(!1);let a;function o(u){cancelAnimationFrame(a),t.value=!0,a=requestAnimationFrame(()=>{a=requestAnimationFrame(()=>{t.value=!1})})}async function m(){await new Promise(u=>requestAnimationFrame(u)),await new Promise(u=>requestAnimationFrame(u)),await new Promise(u=>requestAnimationFrame(u)),await new Promise(u=>{if(t.value){const l=te(t,()=>{l(),u()})}else u()})}async function v(u){var P,V;if(u.key==="Tab"&&((P=n.value)==null||P.focus()),!["PageDown","PageUp","Home","End"].includes(u.key))return;const l=(V=e.value)==null?void 0:V.$el;if(!l)return;(u.key==="Home"||u.key==="End")&&l.scrollTo({top:u.key==="Home"?0:l.scrollHeight,behavior:"smooth"}),await m();const k=l.querySelectorAll(":scope > :not(.v-virtual-scroll__spacer)");if(u.key==="PageDown"||u.key==="Home"){const h=l.getBoundingClientRect().top;for(const x of k)if(x.getBoundingClientRect().top>=h){x.focus();break}}else{const h=l.getBoundingClientRect().bottom;for(const x of[...k].reverse())if(x.getBoundingClientRect().bottom<=h){x.focus();break}}}return{onListScroll:o,onListKeydown:v}}const Fl=W({chips:Boolean,closableChips:Boolean,closeText:{type:String,default:"$vuetify.close"},openText:{type:String,default:"$vuetify.open"},eager:Boolean,hideNoData:Boolean,hideSelected:Boolean,listProps:{type:Object},menu:Boolean,menuIcon:{type:X,default:"$dropdown"},menuProps:{type:Object},multiple:Boolean,noDataText:{type:String,default:"$vuetify.noDataText"},openOnClear:Boolean,itemColor:String,...el({itemChildren:!1})},"Select"),Rl=W({...Fl(),...Ze(rl({modelValue:null,role:"combobox"}),["validationValue","dirty","appendInnerIcon"]),...tl({transition:{component:ll}})},"VSelect"),Ol=Q()({name:"VSelect",props:Rl(),emits:{"update:focused":e=>!0,"update:modelValue":e=>!0,"update:menu":e=>!0},setup(e,n){let{slots:t}=n;const{t:a}=We(),o=ee(),m=ee(),v=ee(),u=ae(e,"menu"),l=S({get:()=>u.value,set:g=>{var c;u.value&&!g&&((c=m.value)!=null&&c.ΨopenChildren)||(u.value=g)}}),{items:k,transformIn:P,transformOut:V}=nl(e),h=ae(e,"modelValue",[],g=>P(g===null?[null]:de(g)),g=>{const c=V(g);return e.multiple?c:c[0]??null}),x=S(()=>typeof e.counterValue=="function"?e.counterValue(h.value):typeof e.counterValue=="number"?e.counterValue:h.value.length),y=cl(),w=S(()=>h.value.map(g=>g.value)),s=L(!1),I=S(()=>l.value?e.closeText:e.openText);let D="",z;const B=S(()=>e.hideSelected?k.value.filter(g=>!h.value.some(c=>e.valueComparator(c,g))):k.value),H=S(()=>e.hideNoData&&!B.value.length||e.readonly||(y==null?void 0:y.isReadonly.value)),p=S(()=>{var g;return{...e.menuProps,activatorProps:{...((g=e.menuProps)==null?void 0:g.activatorProps)||{},"aria-haspopup":"listbox"}}}),d=ee(),{onListScroll:T,onListKeydown:_}=Bl(d,o);function R(g){e.openOnClear&&(l.value=!0)}function O(){H.value||(l.value=!l.value)}function K(g){var C,F;if(!g.key||e.readonly||y!=null&&y.isReadonly.value)return;["Enter"," ","ArrowDown","ArrowUp","Home","End"].includes(g.key)&&g.preventDefault(),["Enter","ArrowDown"," "].includes(g.key)&&(l.value=!0),["Escape","Tab"].includes(g.key)&&(l.value=!1),g.key==="Home"?(C=d.value)==null||C.focus("first"):g.key==="End"&&((F=d.value)==null||F.focus("last"));const c=1e3;function i(A){const G=A.key.length===1,U=!A.ctrlKey&&!A.metaKey&&!A.altKey;return G&&U}if(e.multiple||!i(g))return;const f=performance.now();f-z>c&&(D=""),D+=g.key.toLowerCase(),z=f;const b=k.value.find(A=>A.title.toLowerCase().startsWith(D));if(b!==void 0){h.value=[b];const A=B.value.indexOf(b);se&&window.requestAnimationFrame(()=>{var G;A>=0&&((G=v.value)==null||G.scrollToIndex(A))})}}function j(g){let c=arguments.length>1&&arguments[1]!==void 0?arguments[1]:!0;if(!g.props.disabled)if(e.multiple){const i=h.value.findIndex(b=>e.valueComparator(b.value,g.value)),f=c??!~i;if(~i){const b=f?[...h.value,g]:[...h.value];b.splice(i,1),h.value=b}else f&&(h.value=[...h.value,g])}else{const i=c!==!1;h.value=i?[g]:[],xe(()=>{l.value=!1})}}function $(g){var c;(c=d.value)!=null&&c.$el.contains(g.relatedTarget)||(l.value=!1)}function M(){var g;s.value&&((g=o.value)==null||g.focus())}function ne(g){s.value=!0}function Z(g){if(g==null)h.value=[];else if(ke(o.value,":autofill")||ke(o.value,":-webkit-autofill")){const c=k.value.find(i=>i.title===g);c&&j(c)}else o.value&&(o.value.value="")}return te(l,()=>{if(!e.hideSelected&&l.value&&h.value.length){const g=B.value.findIndex(c=>h.value.some(i=>e.valueComparator(i.value,c.value)));se&&window.requestAnimationFrame(()=>{var c;g>=0&&((c=v.value)==null||c.scrollToIndex(g))})}}),te(()=>e.items,(g,c)=>{l.value||s.value&&!c.length&&g.length&&(l.value=!0)}),le(()=>{const g=!!(e.chips||t.chip),c=!!(!e.hideNoData||B.value.length||t["prepend-item"]||t["append-item"]||t["no-data"]),i=h.value.length>0,f=De.filterProps(e),b=i||!s.value&&e.label&&!e.persistentPlaceholder?void 0:e.placeholder;return r(De,q({ref:o},f,{modelValue:h.value.map(C=>C.props.value).join(", "),"onUpdate:modelValue":Z,focused:s.value,"onUpdate:focused":C=>s.value=C,validationValue:h.externalValue,counterValue:x.value,dirty:i,class:["v-select",{"v-select--active-menu":l.value,"v-select--chips":!!e.chips,[`v-select--${e.multiple?"multiple":"single"}`]:!0,"v-select--selected":h.value.length,"v-select--selection-slot":!!t.selection},e.class],style:e.style,inputmode:"none",placeholder:b,"onClick:clear":R,"onMousedown:control":O,onBlur:$,onKeydown:K,"aria-label":a(I.value),title:a(I.value)}),{...t,default:()=>r(Y,null,[r(al,q({ref:m,modelValue:l.value,"onUpdate:modelValue":C=>l.value=C,activator:"parent",contentClass:"v-select__content",disabled:H.value,eager:e.eager,maxHeight:310,openOnClick:!1,closeOnContentClick:!1,transition:e.transition,onAfterLeave:M},p.value),{default:()=>[c&&r(ol,q({ref:d,selected:w.value,selectStrategy:e.multiple?"independent":"single-independent",onMousedown:C=>C.preventDefault(),onKeydown:_,onFocusin:ne,onScrollPassive:T,tabindex:"-1","aria-live":"polite",color:e.itemColor??e.color},e.listProps),{default:()=>{var C,F,A;return[(C=t["prepend-item"])==null?void 0:C.call(t),!B.value.length&&!e.hideNoData&&(((F=t["no-data"])==null?void 0:F.call(t))??r(Re,{title:a(e.noDataText)},null)),r(_l,{ref:v,renderless:!0,items:B.value},{default:G=>{var we;let{item:U,index:ie,itemRef:N}=G;const Ie=q(U.props,{ref:N,key:ie,onClick:()=>j(U,null)});return((we=t.item)==null?void 0:we.call(t,{item:U,index:ie,props:Ie}))??r(Re,q(Ie,{role:"option"}),{prepend:rt=>{let{isSelected:ct}=rt;return r(Y,null,[e.multiple&&!e.hideSelected?r(Sl,{key:U.value,modelValue:ct,ripple:!1,tabindex:"-1"},null):void 0,U.props.prependAvatar&&r(pe,{image:U.props.prependAvatar},null),U.props.prependIcon&&r(J,{icon:U.props.prependIcon},null)])}})}}),(A=t["append-item"])==null?void 0:A.call(t)]}})]}),h.value.map((C,F)=>{function A(N){N.stopPropagation(),N.preventDefault(),j(C,!1)}const G={"onClick:close":A,onKeydown(N){N.key!=="Enter"&&N.key!==" "||(N.preventDefault(),N.stopPropagation(),A(N))},onMousedown(N){N.preventDefault(),N.stopPropagation()},modelValue:!0,"onUpdate:modelValue":void 0},U=g?!!t.chip:!!t.selection,ie=U?il(g?t.chip({item:C,index:F,props:G}):t.selection({item:C,index:F})):void 0;if(!(U&&!ie))return r("div",{key:C.value,class:"v-select__selection"},[g?t.chip?r(ue,{key:"chip-defaults",defaults:{VChip:{closable:e.closableChips,size:"small",text:C.title}}},{default:()=>[ie]}):r(ot,q({key:"chip",closable:e.closableChips,size:"small",text:C.title,disabled:C.props.disabled},G),null):ie??r("span",{class:"v-select__selection-text"},[C.title,e.multiple&&F<h.value.length-1&&r("span",{class:"v-select__selection-comma"},[Ye(",")])])])})]),"append-inner":function(){var G;for(var C=arguments.length,F=new Array(C),A=0;A<C;A++)F[A]=arguments[A];return r(Y,null,[(G=t["append-inner"])==null?void 0:G.call(t,...F),e.menuIcon?r(J,{class:"v-select__menu-icon",icon:e.menuIcon},null):void 0])}})}),ul({isFocused:s,menu:l,select:j},o)}});export{et as J,Sl as V,El as _,st as a,Oe as b,Ol as c,ot as d,kl as m};
