#include <stdio.h>
#include <math.h>
#include <string.h>
#include <fstream.h>
// ������ʵ��matrix��
// ��matrix��Ķ���
07.#include "matrix.h"
08.
09.matrix::matrix(buffer * b): // ȱʡ���캯��������0��0�пվ���
10. rownum(0),colnum(0),istrans(0),isneg(0),issym(0)
11.{
12. if(b){ // ���û�����b����ʹ��b��Ϊ���ݻ���
13.buf=b;
14. buf->alloc(0);
15. }
16. else // ���򣬲���һ���µĻ��棬����ǵ�ǰȱʡ�Ļ�����
17.buf = getnewbuffer(0);
18.}
19.
20.matrix::matrix(size_t n, buffer * b): // ����n�׵�λ����
21.rownum(n),colnum(1),istrans(0),isneg(0),issym(0)
22.{
23. if(b){ // ���û�����b����ʹ��b��Ϊ���ݻ���
24.buf=b;
25. buf->alloc(n);
26. }
27. else // ���򣬲���һ���µĻ��棬����ǵ�ǰȱʡ�Ļ�����
28.buf = getnewbuffer(n);
29.}
30.
31.matrix unit(size_t n) // ����n�׵�λ����
32.{
33. if(n==0) throw TMESSAGE("n must larger than 0n");
34. matrix m(n,n);
35. for(size_t i=0; i<n; i++)
36. for(size_t j=0; j<n; j++)
37. if(i==j) m.set(i,j,1.0);
38. else m.set(i,j,0.0);
39. m.issym = 1;
40. return m;
41.}
42.
43.
44.matrix::matrix(size_t r, size_t c, buffer * b):
45. rownum(r),colnum(c),istrans(0),isneg(0),issym(0)
46.{
47. if(b){ // ���û�����b����ʹ��b��Ϊ���ݻ���
48.buf=b;
49. buf->alloc(r*c);
50. }
51. else // ���򣬲���һ���µĻ��棬����ǵ�ǰȱʡ�Ļ�����
52.buf = getnewbuffer(r*c);
53.}
54.
55.matrix::matrix(matrix& m) // �������캯��
56.{
57. rownum = m.rownum; // ��������������
58. colnum = m.colnum;
59. istrans = m.istrans; // ����ת�ñ��
60. isneg = m.isneg; // ������ֵ���
61. issym = m.issym; // �����ԳƱ��
62. buf = m.buf; // ����ָ����ͬ�����ָ��
63. buf->refnum++; // �������������1
64.}
65.
66.matrix::matrix(const char * filename, buffer * b): // �������ļ��������
67. istrans(0), isneg(0), issym(0)
68.{
69. char label[10];
70. ifstream in(filename); // ���ļ���
71. in >> label; // �ļ���ʼ������matrix�ؼ���
72. if(strcmp(label, "matrix")!=0) throw TMESSAGE("format error!");
73. in >> rownum >> colnum; // ��ȡ����������
74. if(!in.good()) throw TMESSAGE("read file error!");
75. // ������������
76. if(b) { buf=b;
77. buf->alloc(rownum*colnum);
78. }
79. else buf = getnewbuffer(rownum*colnum);
80. size_t line;
81. for(size_t i=0; i<rownum; i++) { // ���ΰ��ж�ȡ
82.in >> line; // ���к�
83.if(line != i+1) throw TMESSAGE("format error!");
84. in.width(sizeof(label));
85. in >> label; // �кź��ð��
86.if(label[0] != ':') throw TMESSAGE("format error!");
87. DOUBLE a;
88. for(size_t j=0; j<colnum; j++) { // ��ȡһ������
89.in >> a;
90. set(i,j,a);
91. }
92. if(!in.good()) throw TMESSAGE("read file error!");
93. }
94. checksym(); // ����Ƿ�Ϊ�Գ���
95.}
96.
97.matrix::matrix(void * data, size_t r, size_t c, buffer * b):
98. rownum(r),colnum(c),istrans(0),isneg(0),issym(0) // ���ݹ��캯��
99.{
100. if(b){
101. buf=b;
102. buf->alloc(r*c);
103. }
104. else
105. buf = getnewbuffer(r*c);
106. DOUBLE * d = (DOUBLE *)data;
107. for(size_t i=0; i<r*c; i++)  // ����������ݿ��������ԭ���ݵ��ڴ��ǿ����ͷŵ�
108.buf->set(i,d[i]);
109. checksym(); // ����Ƿ�Ϊ�Գ���
110.}
111.
112.DOUBLE matrix::operator()(size_t r, size_t c)
113.{
114. if(r>= rownum || c>= colnum)
115. throw TMESSAGE("Out range!");
116. return value(r,c);
117.}
118.
119.matrix& matrix::operator=(matrix& m)  // ��ֵ����
120.{
121. rownum = m.rownum; //  �����������Ŀ���
122. colnum = m.colnum;
123. istrans = m.istrans; // ת�ñ�־�Ŀ���
124. isneg = m.isneg; // ȡ����־�Ŀ���
125. issym = m.issym; // �ԳƱ�־�Ŀ���
126. if(buf == m.buf)  // ���ԭ������m�Ļ���һ�����򷵻�
127.return (*this);
128. buf->refnum--; // ԭ���治ͬ����ԭ�������������1
129. if(!buf->refnum)delete buf; // ��1���ԭ�������������Ϊ0����ɾ��ԭ����
130. buf = m.buf; // ��ԭ����ָ��ָ��m�Ļ���
131. buf->refnum++; // �»������������1
132. checksym(); // ����Ƿ�Ϊ�Գ���
133. return (*this); // �����Լ�������
134.}
135.
136.matrix& matrix::operator=(DOUBLE a) // ͨ����ֵ���������������Ԫ����Ϊa
137.{
138. if(rownum == 0 || colnum == 0) return (*this);
139. for(size_t i=0; i<rownum; i++)
140. for(size_t j=0; j<colnum; j++)
141. set(i,j,a);
142. if(rownum == colnum)issym = 1;
143. return (*this);
144.}
145.
146.matrix matrix::operator-() // �����󸺣�����������
147.{
148. matrix mm(*this);
149. mm.neg();
150. return mm;
151.}
152.
153.ostream& operator<<(ostream& o, matrix& m) // ����������
154.{
155. // ������ؼ���matrix,Ȼ�����кţ��Ʊ�����кţ�����
156. o << "matrix " << m.rownum << 't' << m.colnum << endl;
157. for(size_t i=0; i<m.rownum; i++) { // �����������
158.o<< (i+1) <<':'; // �кź��ð�š�ע��������к��Ǵ�1��ʼ
159.// ���ڲ���̵��кż�1
160. size_t k=8; // ��������һ�е����ݣ�ÿ��һ�����ֳ����˸�ʱ
161.// �ͻ�һ��
162.for(size_t j=0; j<m.colnum; j++) {
163. o<<'t'<<m.value(i,j);
164. if(--k==0) {
165. k=8;
166. o<<endl;
167. }
168. }
169. o<<endl; // ÿ�н���ʱҲ����
170. }
171. return o;
172.}
173.
174.istream& operator>>(istream& in, matrix& m) // �����������
175.{
176. char label[10];
177. in.width(sizeof(label));
178. in >> label; // ����matrix�ؼ���
179. if(strcmp(label, "matrix")!=0)
180. throw TMESSAGE("format error!n");
181. in >> m.rownum >> m.colnum; // �����кź��к�
182. if(!in.good()) throw TMESSAGE("read file error!");
183. m.buf->refnum--; // ԭ������������1
184. if(!m.buf->refnum) delete m.buf; // ��ԭ����������Ϊ0����ɾȥԭ����
185. m.isneg = m.istrans = 0; // ת�ú�ȡ����־����
186. m.buf = getnewbuffer(m.rownum*m.colnum); // ��ȱʡ��������»���
187. size_t line; // ������������
188. for(size_t i=0; i<m.rownum; i++) {
189. in >> line; // �������к�
190.if(line != i+1) throw TMESSAGE("format error!n");
191. in.width(sizeof(label)); // �кź�Ӧ��ð��
192.in >> label;
193. if(label[0] != ':') throw TMESSAGE("format error!n");
194. DOUBLE a; // ����Ǳ��еĸ�����ֵ
195.for(size_t j=0; j<m.colnum; j++) {
196. in >> a;
197. m.set(i,j,a);
198. }
199. if(!in.good()) throw TMESSAGE("read file error!");
200. }
201. m.checksym(); // ����Ƿ�Ϊ�Գ���
202. return in;
203.}
204.
205.matrix& matrix::operator*=(DOUBLE a) // �������˳���a���������ԭ����
206.{
207. for(size_t i=0; i<rownum; i++)
208. for(size_t j=0; j<colnum; j++)
209. set(i,j,a*value(i,j));
210. return (*this);
211.}
212.
213.matrix matrix::operator*(DOUBLE a) // �������˳���a��ԭ�������ݲ��䣬����һ�¾���
214.{
215. matrix m(rownum, colnum);
216. for(size_t i=0; i<rownum; i++)
217. for(size_t j=0; j<colnum; j++)
218. m.set(i,j,a*value(i,j));
219. return m;
220.}
221.
222.matrix matrix::operator+(matrix& m) // ������ӣ�����һ�µľ��󲢷�����
223.{
224. if(rownum != m.rownum || colnum != m.colnum) // ��Ӧ���б�����ͬ
225.throw TMESSAGE("can not do add of matrixn");
226. matrix mm(rownum, colnum); // ����һͬ�Լ�ͬ�εľ���
227. DOUBLE a;
228. for(size_t i=0; i<rownum; i++) // ���
229. for(size_t j=0; j<colnum; j++)
230. {
231. a = value(i,j)+m.value(i,j);
232. mm.set(i,j,a);
233. }
234. mm.checksym(); // ����Ƿ�Ϊ�Գ���
235. return mm;
236.}
237.
238.matrix& matrix::operator+=(matrix &m) // ������ͣ��Լ����ݸı�Ϊ��
239.{
240. DOUBLE a;
241. for(size_t i=0; i<rownum; i++)
242. for(size_t j=0; j<colnum; j++)
243. {
244. a = value(i,j)+m.value(i,j);
245. set(i,j,a);
246. }
247. checksym(); // ����Ƿ�Ϊ�Գ���
248. return (*this);
249.}
250.
251.matrix matrix::operator+(DOUBLE a) // ����ӳ�����ָÿһԪ�ؼ�һ�̶��ĳ���������
252.// �¾���ԭ���󲻱�
253.{
254. matrix m(rownum, colnum);
255. for(size_t i=0; i<rownum; i++)
256. for(size_t j=0; j<colnum; j++)
257. m.set(i,j,a+value(i,j));
258. return m;
259.}
260.
261.matrix& matrix::operator+=(DOUBLE a) // ��������ӳ������������ݸı�
262.{
263. for(size_t i=0; i<rownum; i++)
264. for(size_t j=0; j<colnum; j++)
265. set(i,j,a+value(i,j));
266. return (*this);
267.}
268.
269.matrix operator-(DOUBLE a, matrix& m) { // ���������󣬲����µľ���
270. return (-m)+a;
271.};
272.
273.
274.matrix matrix::operator-(matrix& m) // ��������������µľ���
275.{
276. matrix mm(*this); // ����һͬ�Լ�ͬ�εľ���
277. mm += (-m); // ������Ӧ�ĸ�����
278. return mm;
279.}
280.
281.matrix& matrix::operator-=(matrix& m) // �������������޸�ԭ����
282.{
283. (*this) += (-m);
284. return (*this);
285.}
286.
287.matrix matrix::operator*(matrix& m) // ������ˣ�ԭ�������ݲ��䣬����һ�¾���
288.{
289. if(colnum != m.rownum) // ���������������
290.throw TMESSAGE("can not multiply!");
291. matrix mm(rownum,m.colnum); // ���㲢����һ��Ҫ��ľ���ų˻�
292. DOUBLE a;
293. for(size_t i=0; i<rownum; i++) // ����˻�
294. for(size_t j=0; j<m.colnum; j++){
295. a = 0.0;
296. for(size_t k=0; k<colnum; k++)
297. a += value(i,k)*m.value(k,j);
298. mm.set(i,j,a);
299. }
300. mm.checksym(); // ����Ƿ�Ϊ�Գ���
301. return mm; // ���س˻�
302.}
303.
304.matrix& matrix::operator*=(matrix& m) // ������ˣ��Լ��޸ĳɻ�����
305.{
306. (*this) = (*this)*m;
307. return (*this);
308.}
309.
310.matrix matrix::t()  // ����ת�ã������µľ���
311.{
312. matrix mm(*this);
313. mm.trans();
314. return mm;
315.}
316.
317.int matrix::isnear(matrix& m, double e) // ���������Ƿ�������
318.{
319. if(rownum != m.rownum || colnum != m.colnum) return 0;
320. for(size_t i=0; i< rownum; i++)
321. for(size_t j=0; j< colnum; j++)
322. if(fabs(value(i,j)-m.value(i,j)) > e) return 0;
323. return 1;
324.}
325.
326.int matrix::isnearunit(double e) // �������Ƿ����Ϊ��λ����
327.{
328. if(rownum != colnum) return 0;
329. return isnear(unit(rownum), e);
330.}
331.
332.matrix matrix::row(size_t r) // ��ȡ��r��������
333.{
334. matrix mm(1, colnum);
335. for(int i=0; i< colnum; i++)
336.mm.set(0, i, value(r,i));
337. return mm;
338.}
339.
340.matrix matrix::col(size_t c) // ��ȡ��c��������
341.{
342. matrix mm(rownum, 1);
343. for(int i=0; i< rownum; i++)
344. mm.set(i, value(i, c));
345. return mm;
346.}
347.
348.void matrix::swapr(size_t r1, size_t r2, size_t k) // ��������r1��r2����
349.{
350. DOUBLE a;
351. for(size_t i=k; i<colnum; i++) {
352. a = value(r1, i);
353. set(r1, i, value(r2, i));
354. set(r2, i, a);
355. }
356.}
357.
358.void matrix::swapc(size_t c1, size_t c2, size_t k) // ����c1��c2����
359.{
360. DOUBLE a;
361. for(size_t i=k; i<colnum; i++) {
362. a = value(i, c1);
363. set(i, c1, value(i, c2));
364. set(i, c2, a);
365. }
366.}
367.
368.DOUBLE matrix::maxabs(size_t &r, size_t &c, size_t k) // ���k�к͵�k�к����Ԫ��λ��
369.{
370. DOUBLE a=0.0;
371. for(size_t i=k;i<rownum;i++)
372. for(size_t j=k;j<colnum;j++)
373. if(a < fabs(value(i,j))) {
374. r=i;c=j;a=fabs(value(i,j));
375. }
376. return a;
377.}
378.
379.size_t matrix::zgsxy(matrix & m, int fn) // ��������˹��Ԫ���㣬fnΪ������ȱʡΪ0
380. /* ��������ʵ�ǳ����󣬶�����m�����Ƿ���
381. ���������ʵ�ǶԱ������mͬʱ���г��ȱ任��
382. ������m�ĶԽ�����˽�������ʽ������������任��
383. �Լ���ԭ����m��������ˣ�m���ȱ����أ�����ȵ��ڽ���
384. �򱾾����е������Ѿ���Ψһ��
385. */
386.{
387. if(rownum != m.rownum || m.rownum != m.colnum) // ����������������m���
388.// ��m�����Ƿ���
389.throw TMESSAGE("can not divide!");
390. lbuffer * bb = getnewlbuffer(rownum); // ����һά��Ϊ�����ĳ�����������
391. lbuffer & b = (*bb); // �����õİ취ʹ����ĳ������׶�
392. size_t is;
393. DOUBLE a;
394. size_t i,j,rank=0;
395. for(size_t k=0; k<rownum; k++) { // �ӵ�0�е���k�н�������˹��Ԫ
396.if(m.maxabs(is, i, k)==0) // ��m�е�k����Ԫ����Ԫ���ڵ��У��д���is,i��
397.break; // �����ԪΪ�㣬��m�����棬����ʧ��
398.rank = k+1; // rank��ŵ�ǰ�Ľ���
399.b.retrieve(k) = i;  // ���������������ĵ�k��ֵ��Ϊi
400. if(i != k)
401. m.swapc(k, i); // ����m��i,k����
402.if(is != k) {
403. m.swapr(k, is, k); // ����m��i,k����,��k���Ժ󽻻�
404.swapr(k, is); // ������������i,k����
405.}
406.a = m.value(k,k);  // ȡ����ԪԪ��
407.for (j=k+1;j<rownum;j++) // �����ǽ�m�ĵ�k�г�����Ԫ
408.// ��ֻ��ѵ�k�еĵ�k+1�����ϳ�����Ԫ����
409.// ��������������Ԫ������ʽ������
410.m.set(k,j,m.value(k,j)/a);
411. for (j=0;j<colnum;j++) // ��������ĵ�k�г�����Ԫ
412.set(k,j,value(k,j)/a);
413. // ���������൱�ڽ�m�ͱ����󹹳ɵ���������k�г�����Ԫ
414.// ���������������л������ȱ任ʹ��k�е������о�Ϊ��
415.// ��0ֵ�ޱ�Ҫ���㣬��˴ӵ�k+1�п�ʼ����
416.for(j=k+1;j<rownum;j++) // j�����У����������������m������
417.for(i=0;i<rownum;i++) //i�����У����ζԸ��м��㣬k�г���
418.if(i!=k)
419. m.set(i,j,m.value(i,j)-m.value(i,k)*m.value(k,j));
420. // �Ա���������ͬ���ļ���
421.for(j=0;j<colnum;j++)
422. for(i=0;i<rownum;i++)
423. if(i!=k)
424. set(i,j,value(i,j)-m.value(i,k)*value(k,j));
425. } // ����˹��Ԫѭ��k����
426. if(fn == 1) {
427. for(j=0; j<rank; j++)
428. for(i=0; i<rownum; i++)
429. if(i==j) m.set(i,i,1.0);
430. else
431. m.set(i,j,0.0);
432. for(k = rank; k>0; k--)
433.m.swapc(k-1,(size_t)b[k-1]);
434. }
435. for(k = rank; k>0; k--) // ���������еĸ��а�b�����ݽ��е���
436.if(b[k-1] != k-1)
437. swapr(k-1,(size_t)b[k-1]); // �н���
438. delete bb; // �ͷų���������
439. return rank; // ����mm����
440.}
441.
442.matrix& matrix::operator/=(matrix m) // �������صĳ�������/=���ⷽ��
443. // ��������Ϊd,�򷽳�Ϊmx=d,���ǽ�д��x=d/m����ʽ��
444. // �����̵Ľ�Ҳ�����d�У���ʵ�ʱ��ʱдd/=m
445.{
446. if(zgsxy(m)!=rownum) // ���Ȳ�����m�Ľ������򷽳��޽�
447.throw TMESSAGE("can not divide!");
448. return *this;
449.}
450.
451.matrix matrix::operator/(matrix m) // ���m�����������¾���
452.{
453. m.inv(); // m�������
454. return (*this)*m;
455.}
456.
457.matrix& matrix::inv() // ��ȫѡ��Ԫ��˹-Լ�����������
458.{
459. if(rownum != colnum || rownum == 0)
460. throw TMESSAGE("Can not calculate inverse");
461. size_t i,j,k;
462.  DOUBLE d,p;
463. lbuffer * isp = getnewlbuffer(rownum); // ����һά��Ϊ�����ĳ�����������
464. lbuffer * jsp = getnewlbuffer(rownum); // ����һά��Ϊ�����ĳ�����������
465. lbuffer& is = *isp; // ʹ������ʹ������������
466. lbuffer& js = *jsp;
467. for(k=0; k<rownum; k++)
468. {
469. d = maxabs(i, j, k); // ȫ��Ԫ��λ�ú�ֵ
470.is[k] = i;
471. js[k] = j;
472. if(d==0.0) {
473. delete isp;
474. delete jsp;
475. throw TMESSAGE("can not inverse");
476. }
477.   if (is[k] != k) swapr(k,(size_t)is[k]);
478.   if (js[k] != k) swapc(k,(size_t)js[k]);
479. p = 1.0/value(k,k);
480. set(k,k,p);
481.   for (j=0; j<rownum; j++)
482.  if (j!=k) set(k,j,value(k,j)*p);
483.   for (i=0; i<rownum; i++)
484.  if (i!=k)
485. for (j=0; j<rownum; j++)
486.   if (j!=k) set(i,j,value(i,j)-value(i,k)*value(k,j));
487.   for (i=0; i<rownum; i++)
488.  if (i!=k) set(i,k,-value(i,k)*p);
489. } // end for k
490.  for (k=rownum; k>0; k--)
491. { if (js[k-1]!=k-1) swapr((size_t)js[k-1], k-1);
492.   if (is[k-1]!=k-1) swapc((size_t)is[k-1], k-1);
493. }
494.  delete isp;
495.  delete jsp;
496. checksym(); // ����Ƿ�Ϊ�Գ���
497.  return (*this);
498.}
499.
500.matrix matrix::operator~() // ������󣬵������¾���
501.{
502. matrix m(*this);
503. m.inv();
504. return m;
505.}
506.
507.matrix operator/(DOUBLE a, matrix& m) // ��������ٳ˳���
508.{
509. matrix mm(m);
510. mm.inv();
511. if(a != 1.0) mm*=a;
512. return mm;
513.}
514.
515.matrix& matrix::operator/=(DOUBLE a) // ����Ԫ�س�a�ĵ���������ı�
516.{
517. return operator*=(1/a);
518.}
519.
520.matrix matrix::operator/(DOUBLE a) // ����Ԫ�س�a�ĵ����������µľ���
521.{
522. matrix m(*this);
523. m/=a;
524. return m;
525.}
526.
527.DOUBLE matrix::det(DOUBLE err) // ������ʽ��ֵ
528.{
529. if(rownum != colnum || rownum == 0)
530. throw TMESSAGE("Can not calculate det");
531. matrix m(*this);
532. size_t rank;
533. return m.detandrank(rank, err);
534.}
535.
536.size_t matrix::rank(DOUBLE err) // ��������
537.{
538. if(rownum==0 || colnum==0)return 0;
539. size_t k;
540. k = rownum > colnum ? colnum : rownum;
541. matrix m(k,k); // ����k�׷���
542. for(size_t i=0; i<k; i++)
543. for(size_t j=0; j<k; j++)
544. m.set(i,j,value(i,j));
545. size_t r;
546. m.detandrank(r, err);
547. return r;
548.}
549.
550.DOUBLE matrix::detandrank(size_t & rank, DOUBLE err) // ���������ʽ����
551.{
552. if(rownum != colnum || rownum == 0)
553. throw TMESSAGE("calculate det and rank error!");
554. size_t i,j,k,is,js;
555. double f,detv,q,d;
556. f=1.0; detv=1.0;
557. rank = 0;
558.  for (k=0; k<rownum-1; k++)
559. {
560. q = maxabs(is, js, k);
561. if(q<err) return 0.0; // ����Ԫ̫С��������ʽ��ֵ����Ϊ��0
562. rank++; // ����1
563. if(is!=k) { f=-f; swapr(k,is,k); }
564. if(js!=k) { f=-f; swapc(k,js,k); }
565. q = value(k,k);
566. detv *= q;
567.   for (i=k+1; i<rownum; i++)
568.  {
569. d=value(i,k)/q;
570. for (j=k+1; j<rownum; j++)
571. set(i,j,value(i,j)-d*value(k,j));
572.  }
573. } // end loop k
574.  q = value(rownum-1,rownum-1);
575.  if(q != 0.0 ) rank++;
576. return f*detv*q;
577.}
578.
579.void matrix::checksym() // ���ͳ��Ե������󵽶Գƾ���
580.{
581. issym = 0; // �ȼ������ǶԳ�
582. if(rownum != colnum) return; // ���в��ȵ�Ȼ���ǶԳƾ���
583. DOUBLE a,b;
584. for(size_t i=1; i<rownum; i++) // �ӵڶ��п�ʼ���
585. for(size_t j=0; j<i; j++) // �ӵ�һ�е���i-1��
586. {
587.a = value(i,j);
588. b = value(j,i);
589. if( fabs(a-b) >= defaulterr ) return; // ���ֲ��Գƣ�����
590.if(a!=b)set(i,j,b); // ����С�ͽ���΢��
591. }
592. issym = 1; // ���϶Գ����׼
593.}
594.
595.void matrix::house(buffer & b, buffer & c)// �ú�˹�ɶ��±任���Գ����Ϊ�Գ�����
596.// ����b�������Խ���Ԫ�أ�c���شζԽ���Ԫ��
597.{
598. if(!issym) throw TMESSAGE("not symatry");
599. size_t i,j,k;
600. DOUBLE h,f,g,h2,a;
601.  for (i=rownum-1; i>0; i--)
602. { h=0.0;
603.   if (i>1)
604.  for (k=0; k<i; k++)
605. { a = value(i,k); h += a*a;}
606.   if (h == 0.0)
607.  { c[i] = 0.0;
608. if (i==1) c[i] = value(i,i-1);
609. b[i] = 0.0;
610.  }
611.   else
612.  { c[i] = sqrt(h);
613. a = value(i,i-1);
614. if (a > 0.0) c[i] = -c[i];
615. h -= a*c[i];
616. set(i,i-1,a-c[i]);
617. f=0.0;
618. for (j=0; j<i; j++)
619.   { set(j,i,value(i,j)/h);
620.  g=0.0;
621.  for (k=0; k<=j; k++)
622. g += value(j,k)*value(i,k);
623.  if(j<=i-2)
624. for (k=j+1; k<i; k++)
625. g += value(k,j)*value(i,k);
626.  c[j] = g/h;
627.  f += g*value(j,i);
628.   }
629. h2=f/(2*h);
630. for (j=0; j<i; j++)
631.   { f=value(i,j);
632.  g=c[j] - h2*f;
633.  c[j] = g;
634.  for (k=0; k<=j; k++)
635. set(j,k, value(j,k)-f*c[k]-g*value(i,k) );
636.   }
637. b[i] = h;
638.  }
639. }
640.  for (i=0; i<=rownum-2; i++) c[i] = c[i+1];
641.  c[rownum-1] = 0.0;
642.  b[0] = 0.0;
643.  for (i=0; i<rownum; i++)
644. { if ((b[i]!=0.0)&&(i>=1))
645.  for (j=0; j<i; j++)
646. { g=0.0;
647.   for (k=0; k<i; k++)
648.  g=g+value(i,k)*value(k,j);
649.   for (k=0; k<i; k++)
650.  set(k,j,value(k,j)-g*value(k,i));
651. }
652.   b[i] = value(i,i);
653.   set(i,i,1.0);
654.   if (i>=1)
655.  for (j=0; j<=i-1; j++)
656. { set(i,j,0.0);
657.   set(j,i,0.0); }
658. }
659.  return;
660.}
661.
662.void matrix::trieigen(buffer& b, buffer& c, size_t l, DOUBLE eps)
663. // �������Խ����ȫ������ֵ����������
664.{ size_t i,j,k,m,it;
665. double d,f,h,g,p,r,e,s;
666. c[rownum-1]=0.0; d=0.0; f=0.0;
667. for (j=0; j<rownum; j++)
668. { it=0;
669.   h=eps*(fabs(b[j])+fabs(c[j]));
670.   if (h>d) d=h;
671.   m=j;
672.   while ((m<rownum)&&(fabs(c[m])>d)) m+=1;
673.   if (m!=j)
674.  { do
675.   { if (it==l) throw TMESSAGE("fial to calculate eigen value");
676.  it += 1;
677.  g=b[j];
678.  p=(b[j+1]-g)/(2.0*c[j]);
679.  r=sqrt(p*p+1.0);
680.  if (p>=0.0) b[j]=c[j]/(p+r);
681.  else b[j]=c[j]/(p-r);
682.  h=g-b[j];
683.  for (i=j+1; i<rownum; i++)
684. b[i]-=h;
685.  f=f+h; p=b[m]; e=1.0; s=0.0;
686.  for (i=m-1; i>=j; i--)
687. { g=e*c[i]; h=e*p;
688.   if (fabs(p)>=fabs(c[i]))
689.  { e=c[i]/p; r=sqrt(e*e+1.0);
690. c[i+1]=s*p*r; s=e/r; e=1.0/r;
691.  }
692.   else
693. { e=p/c[i]; r=sqrt(e*e+1.0);
694. c[i+1]=s*c[i]*r;
695. s=1.0/r; e=e/r;
696.  }
697.   p=e*b[i]-s*g;
698.   b[i+1]=h+s*(e*g+s*b[i]);
699.   for (k=0; k<rownum; k++)
700.  {
701. h=value(k,i+1);
702. set(k,i+1, s*value(k,i)+e*h);;
703. set(k,i,e*value(k,i)-s*h);
704.  }
705.   if(i==0) break;
706. }
707.  c[j]=s*p; b[j]=e*p;
708.   }
709. while (fabs(c[j])>d);
710.  }
711.   b[j]+=f;
712. }
713.  for (i=0; i<=rownum; i++)
714. { k=i; p=b[i];
715.   if (i+1<rownum)
716.  { j=i+1;
717. while ((j<rownum)&&(b[j]<=p))
718.   { k=j; p=b[j]; j++;}
719.  }
720.   if (k!=i)
721.  { b[k]=b[i]; b[i]=p;
722. for (j=0; j<rownum; j++)
723.   { p=value(j,i);
724.  set(j,i,value(j,k));
725.  set(j,k,p);
726.   }
727.  }
728. }
729.}
730.
731.matrix matrix::eigen(matrix & eigenvalue, DOUBLE eps, size_t l)
732.  // ����Գ����ȫ����������������ֵ
733.// ���ذ����ŷŵ�������������eigenvalue������һά����Ϊ��������ֵ
734.// ��ɵ�������
735.{
736. if(!issym) throw TMESSAGE("it is not symetic matrix");
737. eigenvalue = matrix(rownum); // ����n��1������׼����������ֵ
738. matrix m(*this); // �����Լ�����һ�¾���
739. if(rownum == 1) { // ���ֻ��1X1��������������Ϊ1������ֵΪvalue(0,0)
740. m.set(0,0,1.0);
741. eigenvalue.set(0,value(0,0));
742. return m;
743. }
744. buffer * b, *c;
745. b = getnewbuffer(rownum);
746. c = getnewbuffer(rownum);
747. m.house(*b,*c); // ת�������Խ���
748. m.trieigen(*b,*c,l,eps); // �����������������ֵ
749. for(size_t i=0; i<rownum; i++) // ����b�����ݵ�eigenvalue��
750.eigenvalue.set(i,(*b)[i]);
751. return m;
752.}
753.
754.void matrix::hessenberg() // ��һ��ʵ����Լ��Ϊ���겮�����
755.{
756.  size_t i,j,k;
757.  double d,t;
758.  for (k=1; k<rownum-1; k++)
759. { d=0.0;
760.   for (j=k; j<rownum; j++)
761.  { t=value(j,k-1);
762. if (fabs(t)>fabs(d))
763.   { d=t; i=j;}
764.  }
765.   if (fabs(d)!=0.0)
766.  { if (i!=k)
767.   { for (j=k-1; j<rownum; j++)
768. {
769.   t = value(i,j);
770.   set(i,j,value(k,j));
771.   set(k,j,t);
772. }
773.  for (j=0; j<rownum; j++)
774. {
775.   t = value(j,i);
776.   set(j,i,value(j,k));
777.   set(j,k,t);
778. }
779.   }
780. for (i=k+1; i<rownum; i++)
781.   {
782.  t = value(i,k-1)/d;
783.  set(i,k-1,0.0);
784.  for (j=k; j<rownum; j++)
785.   set(i,j,value(i,j)-t*value(k,j));
786.  for (j=0; j<rownum; j++)
787.   set(j,k,value(j,k)+t*value(j,i));
788.   }
789.  }
790. }
791.}
792.
793.void matrix::qreigen(matrix & u1, matrix & v1, size_t jt, DOUBLE eps)
794. // ��һ��ʵ���������������
795.// a��b������rownum��һ�е����������󣬷���������������ʵ�����鲿
796.{
797. matrix a(*this);
798. a.hessenberg(); // ��������겮�����
799. u1 = matrix(rownum);
800. v1 = matrix(rownum);
801. buffer * uu = getnewbuffer(rownum);
802. buffer * vv = getnewbuffer(rownum);
803. buffer &u = *uu;
804. buffer &v = *vv;
805.  size_t m,it,i,j,k,l;
806.  size_t iir,iic,jjr,jjc,kkr,kkc,llr,llc;
807.  DOUBLE b,c,w,g,xy,p,q,r,x,s,e,f,z,y;
808.  it=0; m=rownum;
809.  while (m!=0)
810. { l=m-1;
811.   while ( l>0 && (fabs(a.value(l,l-1))>eps*
812. (fabs(a.value(l-1,l-1))+fabs(a.value(l,l))))) l--;
813.   iir = m-1; iic = m-1;
814.   jjr = m-1; jjc = m-2;
815.   kkr = m-2; kkc = m-1;
816.   llr = m-2; llc = m-2;
817.   if (l==m-1)
818.  { u[m-1]=a.value(m-1,m-1); v[m-1]=0.0;
819. m--; it=0;
820.  }
821.   else if (l==m-2)
822.  { b=-(a.value(iir,iic)+a.value(llr,llc));
823. c=a.value(iir,iic)*a.value(llr,llc)-
824. a.value(jjr,jjc)*a.value(kkr,kkc);
825. w=b*b-4.0*c;
826. y=sqrt(fabs(w));
827. if (w>0.0)
828.   { xy=1.0;
829.  if (b<0.0) xy=-1.0;
830.  u[m-1]=(-b-xy*y)/2.0;
831.  u[m-2]=c/u[m-1];
832.  v[m-1]=0.0; v[m-2]=0.0;
833.   }
834. else
835.   { u[m-1]=-b/2.0; u[m-2]=u[m-1];
836.  v[m-1]=y/2.0; v[m-2]=-v[m-1];
837.   }
838. m=m-2; it=0;
839.  }
840.   else
841.  {
842.  if (it>=jt) {
843. delete uu;
844. delete vv;
845. throw TMESSAGE("fail to calculate eigenvalue");
846.  }
847. it++;
848. for (j=l+2; j<m; j++)
849. a.set(j,j-2,0.0);
850. for (j=l+3; j<m; j++)
851. a.set(j,j-3,0.0);
852. for (k=l; k+1<m; k++)
853.   { if (k!=l)
854. { p=a.value(k,k-1); q=a.value(k+1,k-1);
855.   r=0.0;
856.   if (k!=m-2) r=a.value(k+2,k-1);
857. }
858.  else
859. {
860.   x=a.value(iir,iic)+a.value(llr,llc);
861.   y=a.value(llr,llc)*a.value(iir,iic)-
862. a.value(kkr,kkc)*a.value(jjr,jjc);
863.   iir = l; iic = l;
864.   jjr = l; jjc = l+1;
865.   kkr = l+1; kkc = l;
866.   llr = l+1; llc = l+1;
867.   p=a.value(iir,iic)*(a.value(iir,iic)-x)
868. +a.value(jjr,jjc)*a.value(kkr,kkc)+y;
869.   q=a.value(kkr,kkc)*(a.value(iir,iic)+a.value(llr,llc)-x);
870.   r=a.value(kkr,kkc)*a.value(l+2,l+1);
871. }
872.  if ((fabs(p)+fabs(q)+fabs(r))!=0.0)
873. { xy=1.0;
874.   if (p<0.0) xy=-1.0;
875.   s=xy*sqrt(p*p+q*q+r*r);
876.   if (k!=l) a.set(k,k-1,-s);
877.   e=-q/s; f=-r/s; x=-p/s;
878.   y=-x-f*r/(p+s);
879.   g=e*r/(p+s);
880.   z=-x-e*q/(p+s);
881.   for (j=k; j<m; j++)
882.  {
883. iir = k; iic = j;
884. jjr = k+1; jjc = j;
885. p=x*a.value(iir,iic)+e*a.value(jjr,jjc);
886. q=e*a.value(iir,iic)+y*a.value(jjr,jjc);
887. r=f*a.value(iir,iic)+g*a.value(jjr,jjc);
888. if (k!=m-2)
889.   { kkr = k+2; kkc = j;
890.  p=p+f*a.value(kkr,kkc);
891.  q=q+g*a.value(kkr,kkc);
892.  r=r+z*a.value(kkr,kkc);
893.  a.set(kkr,kkc,r);
894.   }
895. a.set(jjr,jjc,q);
896. a.set(iir,iic,p);
897.  }
898.   j=k+3;
899.   if (j>=m-1) j=m-1;
900.   for (i=l; i<=j; i++)
901.  { iir = i; iic = k;
902. jjr = i; jjc = k+1;
903. p=x*a.value(iir,iic)+e*a.value(jjr,jjc);
904. q=e*a.value(iir,iic)+y*a.value(jjr,jjc);
905. r=f*a.value(iir,iic)+g*a.value(jjr,jjc);
906. if (k!=m-2)
907.   { kkr = i; kkc = k+2;
908.  p=p+f*a.value(kkr,kkc);
909.  q=q+g*a.value(kkr,kkc);
910.  r=r+z*a.value(kkr,kkc);
911.  a.set(kkr,kkc,r);
912.   }
913. a.set(jjr,jjc,q);
914. a.set(iir,iic,p);
915.  }
916. }
917.   }
918.  }
919. }
920. for(i=0;i<rownum;i++) {
921. u1.set(i,u[i]);
922. v1.set(i,v[i]);
923. }
924. delete uu;
925. delete vv;
926.}
927.
928.DOUBLE gassrand(int rr) // ����һ���ֵ��λ�������̬�ֲ������
929.{
930. static DOUBLE r=3.0; // ����
931. if(rr) r = rr;
932. int i,m;
933. DOUBLE s,w,v,t;
934. s=65536.0; w=2053.0; v=13849.0;
935. t=0.0;
936. for (i=1; i<=12; i++)
937. { r=r*w+v; m=(int)(r/s);
938.   r-=m*s; t+=r/s;
939. }
940. t-=6.0;
941. return(t);
942.}
943.
944.gassvector::gassvector(matrix & r): //r�����������Գ���Ϊ��̬���������Э����
945. a(r)
946.{
947. if(r.rownum != r.colnum) throw TMESSAGE("must be a sqare matrix");
948. if(!r.issym) throw TMESSAGE("must be a symetic matrix");
949. matrix evalue;
950. a = a.eigen(evalue);
951. for(size_t i=0; i<a.colnum; i++) {
952. DOUBLE e = sqrt(evalue(i));
953. for(size_t j=0; j<a.rownum; j++)
954. a.set(j,i,a.value(j,i)*e);
955. }
956.}
957.
958.matrix gassvector::operator()(matrix & r) // ���ظ���Э����������̬�������
959.{
960. a = r;
961. matrix evalue;
962. a = a.eigen(evalue);
963. size_t i;
964. for(i=0; i<a.colnum; i++) {
965. DOUBLE e = sqrt(evalue(i));
966. for(size_t j=0; j<a.rownum; j++)
967. a.set(j,i,a.value(j,i)*e);
968. }
969. matrix rr(a.rownum); // ����������
970. for(i=0; i<a.rownum; i++)
971. rr.set(i,gassrand());
972. return a*rr;
973.}
974.
975.matrix gassvector::operator()() // �������趨��Э����������̬�������
976.{
977. matrix rr(a.rownum);
978. for(size_t i=0; i<a.rownum; i++)
979. rr.set(i,gassrand());
980. return a*rr;
981.}
982.
983.void gassvector::setdata(matrix & r) // ����Э������������������
984.{
985. if(!r.issym) throw TMESSAGE("r must be symetic!");
986. a = r;
987. matrix evalue;
988. a = a.eigen(evalue);
989. for(size_t i=0; i<a.colnum; i++) {
990.    if(evalue(i)<0.0) throw TMESSAGE("var matrix not positive!");
991. DOUBLE e = sqrt(evalue(i));
992. for(size_t j=0; j<a.rownum; j++)
993. a.set(j,i,a.value(j,i)*e);
994. }
995.}
996.
997.int matrix::ispositive() // �ж������Ƿ�ԳƷǸ��������Ƿ���1�����򷵻�0
998.{
999. if(!issym) return 0;
1000. matrix ev;
1001. eigen(ev);
1002. for(size_t i=0; i<rownum; i++)
1003. if(ev(i)<0) return 0;
1004. return 1;
1005.}
1006.
1007.matrix matrix::cholesky(matrix& dd) // ������˹���ֽⷨ��Գ������������
1008.// ������ax=d���ط�����Ľ⣬����Ϊa���ı�Ϊ�ֽ���u,d����
1009.{
1010. if(!issym) throw TMESSAGE("not symetic!");
1011. if(dd.rownum != colnum) throw TMESSAGE("dd's rownum not right!");
1012. matrix md(dd);
1013. size_t i,j,k,u,v;
1014. if(value(0,0)<=0.0) throw TMESSAGE("not positive");
1015. set(0,0,sqrt(value(0,0))); //  a[0]=sqrt(a[0]);
1016. buffer& a = (*buf);
1017. buffer& d = (*(md.buf));
1018. size_t n = rownum;
1019. size_t m = dd.colnum;
1020. for (j=1; j<n; j++) a[j]=a[j]/a[0];
1021. for (i=1; i<n; i++)
1022. { u=i*n+i;
1023.   for (j=1; j<=i; j++)
1024.  { v=(j-1)*n+i;
1025. a[u]=a[u]-a[v]*a[v];
1026.  }
1027.   if (a[u]<=0.0) throw TMESSAGE("not positive");
1028.   a[u]=sqrt(a[u]);
1029.   if (i!=(n-1))
1030.  { for (j=i+1; j<n; j++)
1031.   { v=i*n+j;
1032.  for (k=1; k<=i; k++)
1033. a[v]=a[v]-a[(k-1)*n+i]*a[(k-1)*n+j];
1034.  a[v]=a[v]/a[u];
1035.   }
1036.  }
1037. }
1038. for (j=0; j<m; j++)
1039. { d[j]=d[j]/a[0];
1040.   for (i=1; i<=n-1; i++)
1041.  { u=i*n+i; v=i*m+j;
1042. for (k=1; k<=i; k++)
1043.   d[v]=d[v]-a[(k-1)*n+i]*d[(k-1)*m+j];
1044. d[v]=d[v]/a[u];
1045.  }
1046. }
1047. for (j=0; j<=m-1; j++)
1048. { u=(n-1)*m+j;
1049.   d[u]=d[u]/a[n*n-1];
1050.   for (k=n-1; k>=1; k--)
1051.  { u=(k-1)*m+j;
1052. for (i=k; i<=n-1; i++)
1053.   { v=(k-1)*n+i;
1054.  d[u]=d[u]-a[v]*d[i*m+j];
1055.   }
1056. v=(k-1)*n+k-1;
1057. d[u]=d[u]/a[v];
1058.  }
1059. }
1060. if(n>1)
1061. for(j=1; j<n; j++)
1062. for(i=0; i<j; i++)
1063. a[i+j*n]=0.0;
1064. return md;
1065.}
1066.
1067.DOUBLE lineopt(matrix& aa,matrix& bb, matrix& cc, matrix & xx)
1068. // ���Թ滮���ŵ�Ѱ�ҳ���aaΪmXn����ʽԼ���������ϵ������bbΪ����ʽԼ��
1069. // �������Ҷ��Ϊmά������ccΪĿ�꺯��ϵ����nά������xx���ؼ�С�㣬Ϊnά����
1070.{
1071. if(aa.rownum != bb.rownum || aa.colnum != cc.rownum ||
1072. aa.colnum != xx.rownum) throw TMESSAGE("dimenstion not right!");
1073. size_t n=aa.colnum, m=aa.rownum;
1074. size_t i,mn,k,j;
1075. matrix a(m,n+m);
1076. for(i=0;i<m;i++) {
1077. for(j=0;j<n;j++)
1078. a.set(i,j,aa(i,j));
1079. for(j=n;j<n+m;j++)
1080. if(j-n == i) a.set(i,j,1.0);
1081. else a.set(i,j,0.0);
1082. }
1083. matrix c(m+n);
1084. c = 0.0;
1085. for(i=0;i<m;i++)
1086. c.set(i,cc(i));
1087. lbuffer* jjs = getnewlbuffer(m);
1088. lbuffer& js = (*jjs);
1089. DOUBLE s,z,dd,y; //,*p,*d;
1090.
1091. for (i=0; i<m; i++) js[i]=n+i;
1092. matrix p(m,m);
1093. matrix d;
1094. mn=m+n; s=0.0;
1095. matrix x(mn);
1096. while (1)
1097. { for (i=0; i<m; i++)
1098.  for (j=0; j<m; j++)
1099. p.set(i,j,a(i,(size_t)js[j]));
1100.   p.inv();
1101. d = p*a;
1102. x = 0.0;
1103.   for (i=0; i<m; i++)
1104.  { s=0.0;
1105. for (j=0; j<=m-1; j++)
1106. s+=p(i,j)*bb(j);
1107. x.set((size_t)js[i],s);
1108.  }
1109.   k=mn; dd=1.0e-35;
1110.   for (j=0; j<mn; j++)
1111.  { z=0.0;
1112. for (i=0; i<m; i++)
1113. z+=c((size_t)js[i])*d(i,j);
1114. z-=c(j);
1115. if (z>dd) { dd=z; k=j;}
1116.  }
1117.   if (k==mn)
1118.  { s=0.0;
1119. for (j=0; j<n; j++) {
1120. xx.set(j,x(j));
1121. s+=c(j)*x(j);
1122. }
1123. delete jjs;
1124. return s;
1125.  }
1126.   j=m;
1127.   dd=1.0e+20;
1128.   for (i=0; i<=m-1; i++)
1129.  if (d(i,k)>=1.0e-20)   // [i*mn+k]>=1.0e-20)
1130. { y=x(size_t(js[i]))/d(i,k);
1131.   if (y<dd) { dd=y; j=i;}
1132. }
1133.   if (j==m) { delete jjs;
1134. throw TMESSAGE("lineopt failed!");
1135. }
1136.   js[j]=k;
1137. }
1138.}

