import{u as E,a as O,b as P,r as L,d as U,c as m,o as I,e as r,f as M,g as i,w as e,h as G,i as x,U as a,S as p,j as t,t as b,k as o,l as h,E as k}from"./index-c3e40adb.js";import{V as d,a as c}from"./VRow-39a21267.js";const y={class:"text-h5"},N=["innerHTML"],T=t("p",{class:"text-h5 pb-3"}," 系統面的說明 ",-1),$=t("h2",null,"檔案的存儲",-1),S=["src","onclick"],V=t("h2",null,"待完成的事項",-1),v=t("p",{style:{"font-size":"1.2rem"}},[h(" 用戶端： "),t("ul",{class:"pl-3"},[t("li",null," 檔案號的檢核規則(需要檔案室提供規則) "),t("li",null," 報表的使用與輸出(待檔案室有想法後提供意見) ")]),h(" 系統端： "),t("ul",{class:"pl-3"},[t("li",null," 資料庫的備份機制(預計7月底前完成) "),t("li",null," 檔案(pdf)的備份機制(預計8月底前完成) ")])],-1),R={__name:"Doc",setup(B){const A=E();O(),P(),k.JUDGEBOOKFILE;const s=L(U({title:"使用說明",content:`<h2>權限</h2>
					<p>
						
						<table>
							<tr>
								<th></th>
								<th>錄事</th>
								<th>
									書記官
								</th>
								<th>
									紀錄科長
								</th>
								<th>
									檔案室
								</th>
								<th>
									資訊室
								</th>
							</tr>
							<tr>
								<th>股別</th>
								<td>
									所屬股
								</td>
								<td >
									所屬股
								</td>
								<td >
									全部
								</td>
								<td >
									全部
								</td>
								<td >
									全部
								</td>
							</tr>
							<tr>
								<th>操作</th>
								<td>
									<ul style="padding-left: 15px;">
										<li>
											上傳(限所屬股，股別不可空白)
										</li>
										<li>
											修改(自己上傳AND未審核)
										</li>
										<li>
											下載(自己上傳AND未審核)
										</li>
									</ul>
									
								</td>
								<td>
									<ul style="padding-left: 15px;">
										<li>
											上傳(限所屬股，股別不可空白)
										</li>
										<li>
											修改(自己上傳AND未審核)
										</li>
										<li>
											下載(自己上傳AND未審核)
										</li>
									</ul>
									
								</td>
								<td>
									<ul style="padding-left: 15px;">
										<li>
											上傳(全部股別，股別可以空白)
										</li>
										<li>
											修改(所有未審核案件，可以修改股別)
										</li>
										<li>
											下載(所有未審核案件)
										</li>
									</ul>
									
								</td>
								<td>
									<ul style="padding-left: 15px;">
										<li>
											上傳(全部股別，股別可以空白)
										</li>
										<li>
											修改(所有案件，可以修改股別)
										</li>
										<li>
											審核(所有未審核案件，需要檔案號)
										</li>
										<li>
											下載(所有案件)
										</li>
										<li>
											列印報表
										</li>
									</ul>
									
								</td>
								<td>
									<ul style="padding-left: 15px;">
										<li>
											列印報表
										</li>
										<li>
											其餘與紀錄科長相同
										</li>
										
									</ul>
									
								</td>
							</tr>
						</table>
					</p>
					<h2>首頁</h2>
					<p>
						<UPLOAD_IMAGE>index_head.png</UPLOAD_IMAGE>
						<UPLOAD_IMAGE>menu.png</UPLOAD_IMAGE>
					</p>
					<h2>上傳</h2>
					<p>
						<UPLOAD_IMAGE>upload_a.png</UPLOAD_IMAGE>
						<UPLOAD_IMAGE>upload_b.png</UPLOAD_IMAGE>
					</p>
					<h2>修改</h2>
					<p>
						<UPLOAD_IMAGE>update.png</UPLOAD_IMAGE>
					</p>
					<h2>審核(檔案室)</h2>
					<p>
						<UPLOAD_IMAGE>review_a.png</UPLOAD_IMAGE>
						<UPLOAD_IMAGE>review_b.png</UPLOAD_IMAGE>
					</p>`})),f=m(()=>A.state.files_judgebooks.isAdmin);I(()=>{s.content=g(s.content)});function g(l){let n=l.match(/<UPLOAD_IMAGE>(.*?)<\/UPLOAD_IMAGE>/g);return n&&n.forEach(_=>{let u=_.replace(/<\/?UPLOAD_IMAGE>/g,"").trim(),D=`<img class="thumbnail" src="${a}/${u}" alt="" onclick="${p}('${a}/${u}')">`;l=l.replace(_,D)}),l}return(l,n)=>(r(),M("div",null,[i(d,null,{default:e(()=>[i(c,{cols:"12"},{default:e(()=>[t("p",y,b(s.title),1)]),_:1})]),_:1}),i(d,null,{default:e(()=>[i(c,{cols:"12"},{default:e(()=>[t("article",{innerHTML:s.content,style:{"font-size":"1.2rem"}},null,8,N)]),_:1})]),_:1}),f.value?(r(),G(d,{key:0},{default:e(()=>[i(c,{cols:"12"},{default:e(()=>[T,$,t("p",null,[t("img",{class:"thumbnail",src:`${o(a)}/store_tree.png`,alt:"",onclick:`${o(p)}('${o(a)}/store_tree.png')`},null,8,S)]),V,v]),_:1})]),_:1})):x("",!0)]))}};export{R as default};
