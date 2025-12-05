

const allStations = [
  {
    "StationID": 606535390819516493,
    "StationNameAR": "المنيا",
    "StationNameEN": "EL-MINIA"
  },
  {
    "StationID": 606535384603557971,
    "StationNameAR": "راس الحكمه",
    "StationNameEN": "RAS EL HEKMA"
  },
  {
    "StationID": 606535389733191743,
    "StationNameAR": "الكوم الاحمر",
    "StationNameEN": "EL KOM EL AHMAR"
  },
  {
    "StationID": 606535391364776015,
    "StationNameAR": "جرجا",
    "StationNameEN": "GIRGA"
  },
  {
    "StationID": 606535387493433426,
    "StationNameAR": "شبين الكوم",
    "StationNameEN": "SHEBEEN EL KOOM"
  },
  {
    "StationID": 606535387493433427,
    "StationNameAR": "شنوان",
    "StationNameEN": "SHENWAN"
  },
  {
    "StationID": 606535385199149114,
    "StationNameAR": "غبريال",
    "StationNameEN": "GABRIAL"
  },
  {
    "StationID": 606535387493433429,
    "StationNameAR": "منوف",
    "StationNameEN": "MENOUF"
  },
  {
    "StationID": 606535387493433435,
    "StationNameAR": "سمادون",
    "StationNameEN": "SEMADOON"
  },
  {
    "StationID": 606535388084830258,
    "StationNameAR": "القناطر الخيريه",
    "StationNameEN": "EL KANATER EL KHAYRYAH"
  },
  {
    "StationID": 606535384603557972,
    "StationNameAR": "اطنوح",
    "StationNameEN": "ATNOUH"
  },
  {
    "StationID": 606535384603557973,
    "StationNameAR": "سيدي حنيش",
    "StationNameEN": "SIDI HANEISH"
  },
  {
    "StationID": 606535384603557974,
    "StationNameAR": "سيدي حنوش",
    "StationNameEN": "SIDI HANOUSH"
  },
  {
    "StationID": 606535385769574469,
    "StationNameAR": "كفر المسلميه",
    "StationNameEN": "KAFR EL MESALMIA"
  },
  {
    "StationID": 606535390282645560,
    "StationNameAR": "دار السـلام",
    "StationNameEN": "DAR EL SALAM"
  },
  {
    "StationID": 606535391364776027,
    "StationNameAR": "نجع حمادي",
    "StationNameEN": "NAG HAMMADI"
  },
  {
    "StationID": 606535384603557952,
    "StationNameAR": "العامريه",
    "StationNameEN": "EL AMREYA"
  },
  {
    "StationID": 606535391364776008,
    "StationNameAR": "سوهاج",
    "StationNameEN": "SOHAG"
  },
  {
    "StationID": 606535391364775989,
    "StationNameAR": "اسيوط",
    "StationNameEN": "ASYUT"
  },
  {
    "StationID": 606535384603557960,
    "StationNameAR": "الرويسات",
    "StationNameEN": "EL ROWAYSAT"
  },
  {
    "StationID": 606535384603557961,
    "StationNameAR": "العميد",
    "StationNameEN": "EL AMEED"
  },
  {
    "StationID": 606535387493433422,
    "StationNameAR": "البتانون",
    "StationNameEN": "EL BATOON"
  },
  {
    "StationID": 606535384603557968,
    "StationNameAR": "سيدي شبيب",
    "StationNameEN": "SIDI SHBEEB"
  },
  {
    "StationID": 606535387493433433,
    "StationNameAR": "رمله الانجب",
    "StationNameEN": "RAMLA AL ANGAM"
  },
  {
    "StationID": 606535384603557975,
    "StationNameAR": "جراوله",
    "StationNameEN": "GRAWLA"
  },
  {
    "StationID": 606535391914229845,
    "StationNameAR": "الشراونه",
    "StationNameEN": "EL-SHARWNA"
  },
  {
    "StationID": 606535386352582724,
    "StationNameAR": "الشبانات",
    "StationNameEN": "EL-SHBANAT"
  },
  {
    "StationID": 606535386352582753,
    "StationNameAR": "الضبعيه",
    "StationNameEN": "EL  DABAA"
  },
  {
    "StationID": 606535386931396672,
    "StationNameAR": "السويس",
    "StationNameEN": "SUEZ"
  },
  {
    "StationID": 606535391364775990,
    "StationNameAR": "شطب",
    "StationNameEN": "SHATB"
  },
  {
    "StationID": 606535389733191753,
    "StationNameAR": "الفيوم",
    "StationNameEN": "EL FAYOUM"
  },
  {
    "StationID": 606535389179543638,
    "StationNameAR": "كوم حماده",
    "StationNameEN": "KOM HAMADA"
  },
  {
    "StationID": 606535391364776032,
    "StationNameAR": "فاو",
    "StationNameEN": "FAW"
  },
  {
    "StationID": 606535389179543644,
    "StationNameAR": "الطيريه",
    "StationNameEN": "EL TAYRAH"
  },
  {
    "StationID": 606535386352582755,
    "StationNameAR": "سرابيوم",
    "StationNameEN": "SERABYUM"
  },
  {
    "StationID": 606535384603557965,
    "StationNameAR": "سيدي عبد الرحمن",
    "StationNameEN": "SIDI ABD EL RAHMAN"
  },
  {
    "StationID": 606535384603557970,
    "StationNameAR": "فوكه",
    "StationNameEN": "FOKA"
  },
  {
    "StationID": 606535384603557969,
    "StationNameAR": "جلال",
    "StationNameEN": "GALAL"
  },
  {
    "StationID": 606535383991189603,
    "StationNameAR": "الحضره",
    "StationNameEN": "ALHADRAH"
  },
  {
    "StationID": 606535383991189574,
    "StationNameAR": "عرب الرمل",
    "StationNameEN": "ARAB EL-RAMAL"
  },
  {
    "StationID": 606535390819516470,
    "StationNameAR": "بني سويف",
    "StationNameEN": "BENI SUEF"
  },
  {
    "StationID": 606535389179543636,
    "StationNameAR": "صفط العنب",
    "StationNameEN": "SAFT EL ENAB"
  },
  {
    "StationID": 606535389733191738,
    "StationNameAR": "ذات الكوم",
    "StationNameEN": "ZAT EL KOM"
  },
  {
    "StationID": 606535389179543647,
    "StationNameAR": "البريجات",
    "StationNameEN": "EL BRYGAT"
  },
  {
    "StationID": 606535389179543641,
    "StationNameAR": "منشاه ابوريه",
    "StationNameEN": "MANSHAT ABURAYAH"
  },
  {
    "StationID": 606535392467877956,
    "StationNameAR": "اسوان",
    "StationNameEN": "ASWAN"
  },
  {
    "StationID": 606535389733191735,
    "StationNameAR": "القطا",
    "StationNameEN": "EL QTA"
  },
  {
    "StationID": 606535389733191734,
    "StationNameAR": "القطاالبلد",
    "StationNameEN": "EL QTA EL BALAD"
  },
  {
    "StationID": 606535385769574457,
    "StationNameAR": "شبين القناطر",
    "StationNameEN": "SHEBEIN EL QANATER"
  },
  {
    "StationID": 606535385769574464,
    "StationNameAR": "بلبيس",
    "StationNameEN": "BELBEIS"
  },
  {
    "StationID": 606535385769574465,
    "StationNameAR": "اولاد سيف",
    "StationNameEN": "AWLAD SEIF"
  },
  {
    "StationID": 606535387493433420,
    "StationNameAR": "تلا",
    "StationNameEN": "TELLA"
  },
  {
    "StationID": 606534779596177491,
    "StationNameAR": "الزاويه الحمراء",
    "StationNameEN": "EL ZAWYA EL HAMRA"
  },
  {
    "StationID": 606535386352582723,
    "StationNameAR": "الزقازيق",
    "StationNameEN": "EL-ZQAZYQ"
  },
  {
    "StationID": 606535388638478407,
    "StationNameAR": "شربين",
    "StationNameEN": "SHRBIN"
  },
  {
    "StationID": 606535387493433430,
    "StationNameAR": "كفر السنابسه",
    "StationNameEN": "KAFR EL SANABAH"
  },
  {
    "StationID": 606535384603557938,
    "StationNameAR": "الاسكندريه",
    "StationNameEN": "ALEXANDRIA"
  },
  {
    "StationID": 606535385199149113,
    "StationNameAR": "السوق",
    "StationNameEN": "EL SOUK"
  },
  {
    "StationID": 606534187276566625,
    "StationNameAR": "القاهره",
    "StationNameEN": "CAIRO"
  },
  {
    "StationID": 606535386931396700,
    "StationNameAR": "النجيله",
    "StationNameEN": "EL NEGILLA"
  },
  {
    "StationID": 606535392467877944,
    "StationNameAR": "كوم امبو",
    "StationNameEN": "KOM OMBO"
  },
  {
    "StationID": 606535384603557955,
    "StationNameAR": "مطاربرج العرب",
    "StationNameEN": "MATAR BORG EL ARAB"
  },
  {
    "StationID": 606535386931396703,
    "StationNameAR": "جلبانه",
    "StationNameEN": "GELBANA"
  },
  {
    "StationID": 606535386931396701,
    "StationNameAR": "بئر العبد",
    "StationNameEN": "BEER EL ABD"
  },
  {
    "StationID": 606535386931396702,
    "StationNameAR": "بالوظه",
    "StationNameEN": "BALOZA"
  },
  {
    "StationID": 983398255377448963,
    "StationNameAR": "السلام شرق",
    "StationNameEN": "ELSALAM EAST"
  },
  {
    "StationID": 983398255413035011,
    "StationNameAR": "30 يونيه",
    "StationNameEN": "JUNE 30"
  },
  {
    "StationID": 606535386931396698,
    "StationNameAR": "رمانه",
    "StationNameEN": "ROMANA"
  },
  {
    "StationID": 606535386931396696,
    "StationNameAR": "م القنطره شرق",
    "StationNameEN": "EL QANTA5A SHARQ"
  },
  {
    "StationID": 606535389179543635,
    "StationNameAR": "قليشان",
    "StationNameEN": "QELYSHAN"
  },
  {
    "StationID": 985257658210526989,
    "StationNameAR": "السادات",
    "StationNameEN": "SADAT"
  },
  {
    "StationID": 985257658234119949,
    "StationNameAR": "كفر الجزار",
    "StationNameEN": "KAFR EL-GAZZAR"
  },
  {
    "StationID": 606535390282645575,
    "StationNameAR": "الجيزه",
    "StationNameEN": "EL-GIZA"
  },
  {
    "StationID": 985257658148595469,
    "StationNameAR": "صعيد مصر",
    "StationNameEN": "UPPER EGYPT"
  },
  {
    "StationID": 606534779596177504,
    "StationNameAR": "القلج البلد",
    "StationNameEN": "EL QALAG EL BALAD"
  },
  {
    "StationID": 606534779596177505,
    "StationNameAR": "القلج",
    "StationNameEN": "EL QALAG"
  },
  {
    "StationID": 606534779596177506,
    "StationNameAR": "الجبل الاصفر",
    "StationNameEN": "EL GABAL EL ASFAR"
  },
  {
    "StationID": 606535383991189562,
    "StationNameAR": "الشوبك",
    "StationNameEN": "EL SHOBAK"
  },
  {
    "StationID": 606535383991189566,
    "StationNameAR": "ميت حلفا",
    "StationNameEN": "MEET HALFA"
  },
  {
    "StationID": 606535390282645595,
    "StationNameAR": "كفر عمار",
    "StationNameEN": "KAFR AMAR"
  },
  {
    "StationID": 606535388638478430,
    "StationNameAR": "الحناوي",
    "StationNameEN": "EL HENAWY"
  },
  {
    "StationID": 606535388638478431,
    "StationNameAR": "البكاتوش",
    "StationNameEN": "EL BAKATOOSH"
  },
  {
    "StationID": 606535388638478432,
    "StationNameAR": "كفر الجزاير",
    "StationNameEN": "KAFR EL GAZAYER"
  },
  {
    "StationID": 606535388638478434,
    "StationNameAR": "المدينه",
    "StationNameEN": "EL MADINAH"
  },
  {
    "StationID": 606535388638478435,
    "StationNameAR": "جماجمون",
    "StationNameEN": "GAMAGMON"
  },
  {
    "StationID": 606535388638478433,
    "StationNameAR": "شباس",
    "StationNameEN": "SHBASS"
  },
  {
    "StationID": 606535389179543603,
    "StationNameAR": "الرحمانيه",
    "StationNameEN": "EL RAHMANYH"
  },
  {
    "StationID": 606535389179543604,
    "StationNameAR": "الفتح",
    "StationNameEN": "EL FATH"
  },
  {
    "StationID": 606535389179543606,
    "StationNameAR": "الهــــــــواريـه",
    "StationNameEN": "EL HAWARYAH"
  },
  {
    "StationID": 606535389179543608,
    "StationNameAR": "نفره",
    "StationNameEN": "NAFRA"
  },
  {
    "StationID": 606535389179543609,
    "StationNameAR": "كفر بني هلال",
    "StationNameEN": "KAFR BANY HELAL"
  },
  {
    "StationID": 606535383991189572,
    "StationNameAR": "سندهور",
    "StationNameEN": "SINDNHOR"
  },
  {
    "StationID": 606534779596177503,
    "StationNameAR": "23يوليه",
    "StationNameEN": "23 JULY"
  },
  {
    "StationID": 606535383991189585,
    "StationNameAR": "كفر الزيات",
    "StationNameEN": "KAFR EL-ZAIAT"
  },
  {
    "StationID": 606535389733191740,
    "StationNameAR": "الجلاتمه",
    "StationNameEN": "EL GALATMA"
  },
  {
    "StationID": 606535383991189567,
    "StationNameAR": "قليوب",
    "StationNameEN": "QALYUB"
  },
  {
    "StationID": 606535383991189575,
    "StationNameAR": "قويسنا",
    "StationNameEN": "QUESNA"
  },
  {
    "StationID": 606535383991189565,
    "StationNameAR": "شبرا الخيمه",
    "StationNameEN": "SHUBRA EL-KHEIMA"
  },
  {
    "StationID": 606535383991189582,
    "StationNameAR": "طنطا",
    "StationNameEN": "TANTA"
  },
  {
    "StationID": 606535389179543650,
    "StationNameAR": "الاخماس",
    "StationNameEN": "EL AKHMAS"
  },
  {
    "StationID": 606535389179543642,
    "StationNameAR": "الاتحاد",
    "StationNameEN": "EL ETEHAD"
  },
  {
    "StationID": 606535389179543639,
    "StationNameAR": "كفر بولين",
    "StationNameEN": "KAFR BOLEIN"
  },
  {
    "StationID": 606535383991189579,
    "StationNameAR": "ابو مشهور",
    "StationNameEN": "ABU MASHHOUR"
  },
  {
    "StationID": 606535389733191736,
    "StationNameAR": "برقاش",
    "StationNameEN": "BERQASH"
  },
  {
    "StationID": 606535385769574485,
    "StationNameAR": "السنبلاوين",
    "StationNameEN": "EL SENBELAWEEN"
  },
  {
    "StationID": 606535384603557976,
    "StationNameAR": "سملا",
    "StationNameEN": "SAMLA"
  },
  {
    "StationID": 606535383991189573,
    "StationNameAR": "بنها",
    "StationNameEN": "BANHA"
  },
  {
    "StationID": 606535389179543643,
    "StationNameAR": "الطيريه البلد",
    "StationNameEN": "EL TAYRAH EL BALAD"
  },
  {
    "StationID": 606535384603557946,
    "StationNameAR": "جنينه القباري",
    "StationNameEN": "GUNENA EL QABARY"
  },
  {
    "StationID": 606535384603557949,
    "StationNameAR": "م ـ المكس",
    "StationNameEN": "M-EL MAX"
  },
  {
    "StationID": 606535384603557950,
    "StationNameAR": "سيدي مرغب",
    "StationNameEN": "SEYDI MERGHEB"
  },
  {
    "StationID": 606535384603557951,
    "StationNameAR": "عبد القادر",
    "StationNameEN": "ABD EL QADER"
  },
  {
    "StationID": 606535384603557962,
    "StationNameAR": "جباسات العميد",
    "StationNameEN": "GABASAT EL AMEED"
  },
  {
    "StationID": 606535383991189592,
    "StationNameAR": "دمنهور",
    "StationNameEN": "DAMNHOUR"
  },
  {
    "StationID": 606535384603557964,
    "StationNameAR": "تل العيس",
    "StationNameEN": "TAL EL ABAS"
  },
  {
    "StationID": 606535384603557966,
    "StationNameAR": "غزال",
    "StationNameEN": "GHAZAL"
  },
  {
    "StationID": 606535383991189598,
    "StationNameAR": "كفر الدوار",
    "StationNameEN": "KAFR EL-DAUWAR"
  },
  {
    "StationID": 606535389733191732,
    "StationNameAR": "ابو غالب",
    "StationNameEN": "ABU GHALEB"
  },
  {
    "StationID": 606535389733191730,
    "StationNameAR": "بني سلامه",
    "StationNameEN": "BANI SALAMA"
  },
  {
    "StationID": 606535389179543649,
    "StationNameAR": "الطرانه",
    "StationNameEN": "EL TRANA"
  },
  {
    "StationID": 606535385199149112,
    "StationNameAR": "الظاهريه",
    "StationNameEN": "EL ZAHERIA"
  },
  {
    "StationID": 606535385199149115,
    "StationNameAR": "الرمل",
    "StationNameEN": "EL RAML"
  },
  {
    "StationID": 606535385199149116,
    "StationNameAR": "النقراشي باشا",
    "StationNameEN": "EL NOKRASHY PASHA"
  },
  {
    "StationID": 606535385199149118,
    "StationNameAR": "العصافره",
    "StationNameEN": "EL ASAFRA"
  },
  {
    "StationID": 606535385199149119,
    "StationNameAR": "المندره",
    "StationNameEN": "EL MANDARA"
  },
  {
    "StationID": 606535385199149120,
    "StationNameAR": "المنتزه",
    "StationNameEN": "EL MONTAZA"
  },
  {
    "StationID": 606535385199149121,
    "StationNameAR": "الاصلاح",
    "StationNameEN": "EL ESLAH"
  },
  {
    "StationID": 606535385199149125,
    "StationNameAR": "الكليه البحريه",
    "StationNameEN": "EL KOLIA EL BAHARIA"
  },
  {
    "StationID": 606535385199149126,
    "StationNameAR": "الطابيه",
    "StationNameEN": "EL TABYA"
  },
  {
    "StationID": 606535385199149127,
    "StationNameAR": "سماد ابوقير",
    "StationNameEN": "SEMAD ABU QEER"
  },
  {
    "StationID": 606535385199149128,
    "StationNameAR": "الطرح",
    "StationNameEN": "EL TARH"
  },
  {
    "StationID": 606535385199149129,
    "StationNameAR": "المعديه",
    "StationNameEN": "EL MADIA"
  },
  {
    "StationID": 606535389733191745,
    "StationNameAR": "بشتيل",
    "StationNameEN": "BASHTEEL"
  },
  {
    "StationID": 606535389179543645,
    "StationNameAR": "ابو الخاوي",
    "StationNameEN": "ABU EL KHAWY"
  },
  {
    "StationID": 606535389179543629,
    "StationNameAR": "منشيه البدراوي",
    "StationNameEN": "MANSHEYET EL BADTRAWY"
  },
  {
    "StationID": 606535389179543634,
    "StationNameAR": "المجديه",
    "StationNameEN": "EL MAGDEYA"
  },
  {
    "StationID": 606535389179543632,
    "StationNameAR": "بسنديله",
    "StationNameEN": "BASANDILAH"
  },
  {
    "StationID": 606535388638478428,
    "StationNameAR": "قلـــين البــــلد",
    "StationNameEN": "KALEEN EL BALAD"
  },
  {
    "StationID": 606535389179543640,
    "StationNameAR": "واقد",
    "StationNameEN": "WAQED"
  },
  {
    "StationID": 606535389179543637,
    "StationNameAR": "النقيدي",
    "StationNameEN": "EL NQYEDY"
  },
  {
    "StationID": 606535388638478422,
    "StationNameAR": "ابشواي غربيه",
    "StationNameEN": "EBSHWAY GHARIBA"
  },
  {
    "StationID": 606535385199149130,
    "StationNameAR": "الابعديه",
    "StationNameEN": "EL ABADIA"
  },
  {
    "StationID": 606535385199149131,
    "StationNameAR": "بحيره ادكو",
    "StationNameEN": "BOHAYRET EDKO"
  },
  {
    "StationID": 606535385199149132,
    "StationNameAR": "الدمياطي المستجده",
    "StationNameEN": "EL DOMYATTY EL MOSTAGADA"
  },
  {
    "StationID": 606535385199149133,
    "StationNameAR": "ادكو",
    "StationNameEN": "EDKO"
  },
  {
    "StationID": 606535385199149134,
    "StationNameAR": "مياح",
    "StationNameEN": "MYAH"
  },
  {
    "StationID": 606535385199149135,
    "StationNameAR": "منشيه الامل",
    "StationNameEN": "MANSHEYET EL AML"
  },
  {
    "StationID": 606535385199149136,
    "StationNameAR": "البصيلي",
    "StationNameEN": "EL BESILY"
  },
  {
    "StationID": 606535385199149137,
    "StationNameAR": "الزراعه",
    "StationNameEN": "EL ZERAAH"
  },
  {
    "StationID": 606535385199149138,
    "StationNameAR": "برج رشيد",
    "StationNameEN": "BORG RASHEED"
  },
  {
    "StationID": 606535385199149140,
    "StationNameAR": "الحماد",
    "StationNameEN": "EL HAMAD"
  },
  {
    "StationID": 606535385199149141,
    "StationNameAR": "محله الامير",
    "StationNameEN": "MAHALET EL AMIR"
  },
  {
    "StationID": 606535385199149142,
    "StationNameAR": "تقاطع ادفينا",
    "StationNameEN": "TAQATO EDFEENA"
  },
  {
    "StationID": 606535385199149143,
    "StationNameAR": "ادفينا الجديده",
    "StationNameEN": "EDFEENA EL GADEEDA"
  },
  {
    "StationID": 606535385199149144,
    "StationNameAR": "مطوبس",
    "StationNameEN": "MATOBAS"
  },
  {
    "StationID": 606535385199149145,
    "StationNameAR": "الخيريه",
    "StationNameEN": "EL KHAYRIA"
  },
  {
    "StationID": 606535385199149146,
    "StationNameAR": "شمشيره",
    "StationNameEN": "SHAMSHIRA"
  },
  {
    "StationID": 606535385199149149,
    "StationNameAR": "منيه الاشراف",
    "StationNameEN": "MENYET EL ASHRAF"
  },
  {
    "StationID": 606535385199149150,
    "StationNameAR": "قبريط",
    "StationNameEN": "QABREET"
  },
  {
    "StationID": 606535385769574455,
    "StationNameAR": "طحانوب",
    "StationNameEN": "TAHANOUB"
  },
  {
    "StationID": 606535385199149151,
    "StationNameAR": "السلميه بحري",
    "StationNameEN": "EL SELMIA EL BAHARIA"
  },
  {
    "StationID": 606535385199149152,
    "StationNameAR": "محله مالك",
    "StationNameEN": "MAHLET MALEK"
  },
  {
    "StationID": 606535385199149153,
    "StationNameAR": "السعاده",
    "StationNameEN": "EL SAADA"
  },
  {
    "StationID": 606535385199149154,
    "StationNameAR": "الدوايده",
    "StationNameEN": "EL DAWAYDA"
  },
  {
    "StationID": 606535385199149155,
    "StationNameAR": "ابو غنيمه",
    "StationNameEN": "ABU GHENIMA"
  },
  {
    "StationID": 606535385769574450,
    "StationNameAR": "القصابي بحري",
    "StationNameEN": "EL QASABY BAHARY"
  },
  {
    "StationID": 606535385769574451,
    "StationNameAR": "كفر رماده",
    "StationNameEN": "KAFR RAMADA"
  },
  {
    "StationID": 606535385769574453,
    "StationNameAR": "الزهويين",
    "StationNameEN": "EL ZAHAWEEN"
  },
  {
    "StationID": 606535385769574454,
    "StationNameAR": "كفر طحا",
    "StationNameEN": "KAFR TAHA"
  },
  {
    "StationID": 606535385769574456,
    "StationNameAR": "كفر شبين",
    "StationNameEN": "KAFR SHEBEIN"
  },
  {
    "StationID": 606535385769574458,
    "StationNameAR": "منشاه الكرام",
    "StationNameEN": "MANSHEYET EL KERAM"
  },
  {
    "StationID": 606535385769574460,
    "StationNameAR": "سلمنت",
    "StationNameEN": "SELMENT"
  },
  {
    "StationID": 606535385769574462,
    "StationNameAR": "بير عماره",
    "StationNameEN": "BEER AMARAH"
  },
  {
    "StationID": 606535385769574463,
    "StationNameAR": "تل روزن",
    "StationNameEN": "TAL ROSEN"
  },
  {
    "StationID": 606535385769574452,
    "StationNameAR": "نوي",
    "StationNameEN": "NOWY"
  },
  {
    "StationID": 606535390282645593,
    "StationNameAR": "المتانيه",
    "StationNameEN": "EL-MATANIA"
  },
  {
    "StationID": 606535385769574468,
    "StationNameAR": "العصلوجي",
    "StationNameEN": "EL ASLOOGY"
  },
  {
    "StationID": 606535385199149139,
    "StationNameAR": "رشيد",
    "StationNameEN": "RASHEED"
  },
  {
    "StationID": 606535388638478391,
    "StationNameAR": "سمنود",
    "StationNameEN": "SAMNOOD"
  },
  {
    "StationID": 606535388638478396,
    "StationNameAR": "طلخا",
    "StationNameEN": "TALHA"
  },
  {
    "StationID": 606535388638478416,
    "StationNameAR": "كفر البطيخ",
    "StationNameEN": "KAFR EL BATEEKH"
  },
  {
    "StationID": 606535388638478417,
    "StationNameAR": "بساتين كفر البطيخ",
    "StationNameEN": "BASATEEN KAFR EL BATEEKH"
  },
  {
    "StationID": 606535385769574470,
    "StationNameAR": "هـــريه رزنـــه",
    "StationNameEN": "HERYET RESNA"
  },
  {
    "StationID": 606535385769574471,
    "StationNameAR": "العداويه",
    "StationNameEN": "EL ADAWYA"
  },
  {
    "StationID": 606535385769574475,
    "StationNameAR": "ابو ياسين",
    "StationNameEN": "ABU YASSEIN"
  },
  {
    "StationID": 606535385769574476,
    "StationNameAR": "ابو كبير",
    "StationNameEN": "ABU KEBEER"
  },
  {
    "StationID": 606535385769574477,
    "StationNameAR": "نزله خيال",
    "StationNameEN": "NEZLET EL KHAYAL"
  },
  {
    "StationID": 606535385769574478,
    "StationNameAR": "البوها",
    "StationNameEN": "EL BOOHA"
  },
  {
    "StationID": 606535385769574479,
    "StationNameAR": "كفر صقر",
    "StationNameEN": "KAFR SAQR"
  },
  {
    "StationID": 606535385769574480,
    "StationNameAR": "بدوي",
    "StationNameEN": "BADAWY"
  },
  {
    "StationID": 606535385769574481,
    "StationNameAR": "ابو الشقوق",
    "StationNameEN": "ABU EL SHEQOQ"
  },
  {
    "StationID": 606535385769574482,
    "StationNameAR": "هيكل باشا",
    "StationNameEN": "HEIKAL PASHA"
  },
  {
    "StationID": 606535385769574484,
    "StationNameAR": "طرانيس العرب",
    "StationNameEN": "TARANEES EL ARAB"
  },
  {
    "StationID": 606535385769574486,
    "StationNameAR": "شبرا قباله",
    "StationNameEN": "SHOBRA QEBALA"
  },
  {
    "StationID": 606535385769574487,
    "StationNameAR": "الزريقي",
    "StationNameEN": "EL ZOREEKY"
  },
  {
    "StationID": 606535385769574488,
    "StationNameAR": "البقليه",
    "StationNameEN": "EL BAQLIA"
  },
  {
    "StationID": 606535385769574489,
    "StationNameAR": "شاوه",
    "StationNameEN": "SHAW"
  },
  {
    "StationID": 606535385769574490,
    "StationNameAR": "سندوب",
    "StationNameEN": "SANDOUB"
  },
  {
    "StationID": 606535385769574492,
    "StationNameAR": "خلفالله",
    "StationNameEN": "KHALAF ALLAH"
  },
  {
    "StationID": 606535385769574493,
    "StationNameAR": "الفدادنه",
    "StationNameEN": "EL FADADNA"
  },
  {
    "StationID": 606535385769574494,
    "StationNameAR": "البيروم",
    "StationNameEN": "EL BAIROUM"
  },
  {
    "StationID": 606535385769574496,
    "StationNameAR": "جهينه",
    "StationNameEN": "GUHAYNA"
  },
  {
    "StationID": 606535385769574497,
    "StationNameAR": "كفر الحاج عمر",
    "StationNameEN": "KAFR EL HAG OMAR"
  },
  {
    "StationID": 606535385769574499,
    "StationNameAR": "الدراكه",
    "StationNameEN": "EL DRAKA"
  },
  {
    "StationID": 606535386931396661,
    "StationNameAR": "فايد",
    "StationNameEN": "FAYED"
  },
  {
    "StationID": 606535386352582706,
    "StationNameAR": "اباظه",
    "StationNameEN": "ABAZA"
  },
  {
    "StationID": 606535386352582708,
    "StationNameAR": "الكبايشه",
    "StationNameEN": "EL KABAYSHA"
  },
  {
    "StationID": 606535386352582710,
    "StationNameAR": "اشكر",
    "StationNameEN": "ASHKOR"
  },
  {
    "StationID": 606535386352582711,
    "StationNameAR": "الشهيد عبدالمنعم رياص",
    "StationNameEN": "EL SHAHEED ABD EL MONIEM REYAD"
  },
  {
    "StationID": 606535386352582712,
    "StationNameAR": "الســــــعادنه",
    "StationNameEN": "EL SAADNA"
  },
  {
    "StationID": 606535386352582713,
    "StationNameAR": "السماعنه",
    "StationNameEN": "EL SAMAANA"
  },
  {
    "StationID": 606535385769574495,
    "StationNameAR": "فاقوس",
    "StationNameEN": "FAKOUS"
  },
  {
    "StationID": 606535386352582716,
    "StationNameAR": "العزيزيه",
    "StationNameEN": "EL-AZIZIA"
  },
  {
    "StationID": 606535386352582743,
    "StationNameAR": "البلاح",
    "StationNameEN": "EL-BALAH"
  },
  {
    "StationID": 606535386352582746,
    "StationNameAR": "التينه",
    "StationNameEN": "EL-TENNA"
  },
  {
    "StationID": 606535386931396663,
    "StationNameAR": "قمه فايد",
    "StationNameEN": "QMET FAYED"
  },
  {
    "StationID": 606535386931396664,
    "StationNameAR": "فناره",
    "StationNameEN": "FANARAH"
  },
  {
    "StationID": 606535386352582731,
    "StationNameAR": "القصاصين",
    "StationNameEN": "EL-KSASIN"
  },
  {
    "StationID": 606535386352582751,
    "StationNameAR": "بورسعيد",
    "StationNameEN": "PORT SAID"
  },
  {
    "StationID": 606535386352582719,
    "StationNameAR": "منيه القمح",
    "StationNameEN": "MNYH ALQMH"
  },
  {
    "StationID": 606535386352582729,
    "StationNameAR": "التل الكبير",
    "StationNameEN": "EL-TAL EL-KABEER"
  },
  {
    "StationID": 606535386352582740,
    "StationNameAR": "الشيخ زايد",
    "StationNameEN": "EL-SHIKH ZAYED"
  },
  {
    "StationID": 606535386352582748,
    "StationNameAR": "راس العش",
    "StationNameEN": "RAS EL-ESH"
  },
  {
    "StationID": 606535386931396665,
    "StationNameAR": "كسفريت",
    "StationNameEN": "KASFREET"
  },
  {
    "StationID": 606535386931396667,
    "StationNameAR": "ابو حلب",
    "StationNameEN": "ABU HALAB"
  },
  {
    "StationID": 606535386931396668,
    "StationNameAR": "شلوفه",
    "StationNameEN": "SHLOUFA"
  },
  {
    "StationID": 606535386931396669,
    "StationNameAR": "الجناين",
    "StationNameEN": "EL GANAYEN"
  },
  {
    "StationID": 606535386931396670,
    "StationNameAR": "عامـــــر",
    "StationNameEN": "AMER"
  },
  {
    "StationID": 606535389179543630,
    "StationNameAR": "بلقاس",
    "StationNameEN": "BELKASS"
  },
  {
    "StationID": 606535386931396684,
    "StationNameAR": "كيلو 15",
    "StationNameEN": "KILO 15"
  },
  {
    "StationID": 606535386931396685,
    "StationNameAR": "درب الحاج",
    "StationNameEN": "DARB EL HAG"
  },
  {
    "StationID": 606535386931396687,
    "StationNameAR": "الربيكي",
    "StationNameEN": "EL REBEEKY"
  },
  {
    "StationID": 606535386931396688,
    "StationNameAR": "وادي السيل",
    "StationNameEN": "WADI EL SEIL"
  },
  {
    "StationID": 606535386931396689,
    "StationNameAR": "جبل الجفره",
    "StationNameEN": "GABAL EL GAFRA"
  },
  {
    "StationID": 606535386352582745,
    "StationNameAR": "الكاب",
    "StationNameEN": "EL-KAB"
  },
  {
    "StationID": 606535387493433400,
    "StationNameAR": "وروره",
    "StationNameEN": "WARWARAH"
  },
  {
    "StationID": 606535387493433401,
    "StationNameAR": "دملو",
    "StationNameEN": "DEMLO"
  },
  {
    "StationID": 606535387493433402,
    "StationNameAR": "ميت الحوفيين",
    "StationNameEN": "MEET EL HOFEYEEN"
  },
  {
    "StationID": 606535387493433403,
    "StationNameAR": "ميت بره",
    "StationNameEN": "MEET BARA"
  },
  {
    "StationID": 606535387493433404,
    "StationNameAR": "ه.بقسا",
    "StationNameEN": "BEQSA"
  },
  {
    "StationID": 606535387493433405,
    "StationNameAR": "شبرابم",
    "StationNameEN": "SHOBRABAKHOM"
  },
  {
    "StationID": 606535387493433406,
    "StationNameAR": "تفهنا العزب",
    "StationNameEN": "TAFEHNA EL AZAB"
  },
  {
    "StationID": 606535387493433407,
    "StationNameAR": "ميت الحارون",
    "StationNameEN": "MEET EL HAROON"
  },
  {
    "StationID": 606535387493433408,
    "StationNameAR": "سعد باشا زغلول",
    "StationNameEN": "SAAD PASHA ZAGLOUL"
  },
  {
    "StationID": 606535387493433409,
    "StationNameAR": "اسماعيل باشا صدقي",
    "StationNameEN": "ISMAEEL PASHA SEDKY"
  },
  {
    "StationID": 606535387493433410,
    "StationNameAR": "منصور باشا",
    "StationNameEN": "MANSOUR PASHA"
  },
  {
    "StationID": 606535387493433411,
    "StationNameAR": "كفر بطا",
    "StationNameEN": "KAFR BATTA"
  },
  {
    "StationID": 606535387493433412,
    "StationNameAR": "اسطنها",
    "StationNameEN": "ESTANHA"
  },
  {
    "StationID": 606535387493433413,
    "StationNameAR": "ميت الوسطي",
    "StationNameEN": "MEET EL WASTA"
  },
  {
    "StationID": 606535387493433414,
    "StationNameAR": "سبك الضحاك",
    "StationNameEN": "SOBK EL DAHAK"
  },
  {
    "StationID": 606535387493433415,
    "StationNameAR": "الباجور",
    "StationNameEN": "EL BAGOOR"
  },
  {
    "StationID": 606535387493433416,
    "StationNameAR": "جروان",
    "StationNameEN": "GERWAN"
  },
  {
    "StationID": 606535387493433417,
    "StationNameAR": "كفر شبرا زنجي",
    "StationNameEN": "KAFR SHOBRA ZENGY"
  },
  {
    "StationID": 606535387493433418,
    "StationNameAR": "سرس الليانه",
    "StationNameEN": "SERS EL LAYANA"
  },
  {
    "StationID": 606535387493433419,
    "StationNameAR": "كفر سليم",
    "StationNameEN": "KAFR SELEEM"
  },
  {
    "StationID": 606535387493433421,
    "StationNameAR": "كفر طبلوها",
    "StationNameEN": "KAFR TABLOHA"
  },
  {
    "StationID": 606535387493433423,
    "StationNameAR": "كفرالبتانون وخليل",
    "StationNameEN": "KAFR EL ALBATOON WA KHALIL"
  },
  {
    "StationID": 606535387493433431,
    "StationNameAR": "كمشوش",
    "StationNameEN": "KAMSHOUSH"
  },
  {
    "StationID": 606535387493433434,
    "StationNameAR": "جامع بدر",
    "StationNameEN": "GAMEEA BADR"
  },
  {
    "StationID": 606535387493433438,
    "StationNameAR": "الحلواصي",
    "StationNameEN": "EL HALWASY"
  },
  {
    "StationID": 606535387493433439,
    "StationNameAR": "الحلواصي بلد",
    "StationNameEN": "EL HELWASY BALAD"
  },
  {
    "StationID": 606535387493433437,
    "StationNameAR": "محلة سبك",
    "StationNameEN": "MEHALSABEK"
  },
  {
    "StationID": 606535387493433440,
    "StationNameAR": "شطانوف",
    "StationNameEN": "SHATANOOF"
  },
  {
    "StationID": 606535387493433441,
    "StationNameAR": "كفر صراوه",
    "StationNameEN": "KAFR SARAWAH"
  },
  {
    "StationID": 606535387493433442,
    "StationNameAR": "دروه",
    "StationNameEN": "DERWAH"
  },
  {
    "StationID": 606535388084830259,
    "StationNameAR": "شلقان",
    "StationNameEN": "SHELFAN"
  },
  {
    "StationID": 606535388084830260,
    "StationNameAR": "قليوب البلد",
    "StationNameEN": "QALYUB EL BALAD"
  },
  {
    "StationID": 606535388084830261,
    "StationNameAR": "سانجرج",
    "StationNameEN": "SAGEREG"
  },
  {
    "StationID": 606535388084830262,
    "StationNameAR": "منشيه سلطان",
    "StationNameEN": "MANSHEYET SULTAN"
  },
  {
    "StationID": 606535388084830263,
    "StationNameAR": "العراقيه",
    "StationNameEN": "EL ERAQYA"
  },
  {
    "StationID": 606535388084830264,
    "StationNameAR": "عشما",
    "StationNameEN": "ASHMA"
  },
  {
    "StationID": 606535388084830265,
    "StationNameAR": "الشهداء",
    "StationNameEN": "EL SHOHADAA"
  },
  {
    "StationID": 606535388084830266,
    "StationNameAR": "دنشواي",
    "StationNameEN": "DENSHWAY"
  },
  {
    "StationID": 606535388084830267,
    "StationNameAR": "دناصور",
    "StationNameEN": "DAYNASOUR"
  },
  {
    "StationID": 606535388084830268,
    "StationNameAR": "زاويه البقلي",
    "StationNameEN": "ZAWYA EL BAKLY"
  },
  {
    "StationID": 606535388084830269,
    "StationNameAR": "بشتامي",
    "StationNameEN": "BESHTAMY"
  },
  {
    "StationID": 606535388084830270,
    "StationNameAR": "عمروس",
    "StationNameEN": "AMROOS"
  },
  {
    "StationID": 606535388084830272,
    "StationNameAR": "طنوب",
    "StationNameEN": "TANOUB"
  },
  {
    "StationID": 606535388084830273,
    "StationNameAR": "عزبه الحطيم",
    "StationNameEN": "EZBET EL HATIIM"
  },
  {
    "StationID": 606535388084830274,
    "StationNameAR": "مشلا",
    "StationNameEN": "MESHLA"
  },
  {
    "StationID": 606535388084830275,
    "StationNameAR": "كفر مشله",
    "StationNameEN": "KAFR MESHLA"
  },
  {
    "StationID": 606535388084830276,
    "StationNameAR": "قصر نصر الدين",
    "StationNameEN": "QASR NASR EL DEEN"
  },
  {
    "StationID": 606535388084830277,
    "StationNameAR": "الدلجمون",
    "StationNameEN": "EL DELGAMOON"
  },
  {
    "StationID": 606535388084830278,
    "StationNameAR": "ميت حبيش القبليه",
    "StationNameEN": "MEET HEBESH EL QEBLYA"
  },
  {
    "StationID": 606535388084830279,
    "StationNameAR": "شبرا قاص",
    "StationNameEN": "SHOBRA QAS"
  },
  {
    "StationID": 606535388084830281,
    "StationNameAR": "المنشاه الكبري",
    "StationNameEN": "ELMANSHYET EL KOBRA"
  },
  {
    "StationID": 606535388084830282,
    "StationNameAR": "السملاويه",
    "StationNameEN": "ELSAMALAWAYA"
  },
  {
    "StationID": 606535388084830283,
    "StationNameAR": "نهطاي",
    "StationNameEN": "NAHTAY"
  },
  {
    "StationID": 606535388084830286,
    "StationNameAR": "كوم النور",
    "StationNameEN": "KOM EL NOOR"
  },
  {
    "StationID": 606535388084830289,
    "StationNameAR": "كفر الوزير",
    "StationNameEN": "KAFR EL WAZEER"
  },
  {
    "StationID": 606535388084830290,
    "StationNameAR": "ميت القرش",
    "StationNameEN": "MEET EL QERSH"
  },
  {
    "StationID": 606535391364775998,
    "StationNameAR": "مشطا",
    "StationNameEN": "MASHTAA"
  },
  {
    "StationID": 606535391364775999,
    "StationNameAR": "شطوره",
    "StationNameEN": "SHATOORA"
  },
  {
    "StationID": 606535388084830291,
    "StationNameAR": "ميت ابو العربى",
    "StationNameEN": "MEET ABU ARABI"
  },
  {
    "StationID": 606535388084830292,
    "StationNameAR": "ام الزين",
    "StationNameEN": "OM EL ZEIN"
  },
  {
    "StationID": 606535388084830293,
    "StationNameAR": "الحلبي",
    "StationNameEN": "ELHALABY"
  },
  {
    "StationID": 606535388084830294,
    "StationNameAR": "النخاس",
    "StationNameEN": "EL NAKHAS"
  },
  {
    "StationID": 606535388084830284,
    "StationNameAR": "زفتي",
    "StationNameEN": "ZEFTA"
  },
  {
    "StationID": 606535388084830280,
    "StationNameAR": "السنطه",
    "StationNameEN": "EL SANTA"
  },
  {
    "StationID": 606535388084830295,
    "StationNameAR": "كفرالاشراف",
    "StationNameEN": "KAFR EL ASHRAF"
  },
  {
    "StationID": 606535388084830296,
    "StationNameAR": "شيبه النكاريه",
    "StationNameEN": "SHABEEH EL NAKARYAH"
  },
  {
    "StationID": 606535388084830297,
    "StationNameAR": "القرشيه",
    "StationNameEN": "EL QERSHEYAH"
  },
  {
    "StationID": 606535388084830298,
    "StationNameAR": "منيه البندره",
    "StationNameEN": "MENYAH EL BANDARAH"
  },
  {
    "StationID": 606535388084830299,
    "StationNameAR": "الجميزه",
    "StationNameEN": "EL GEMEIZAH"
  },
  {
    "StationID": 606535388084830300,
    "StationNameAR": "شندلات",
    "StationNameEN": "SHANDALAT"
  },
  {
    "StationID": 606535388084830302,
    "StationNameAR": "الرجديه",
    "StationNameEN": "EL-RAGDIA"
  },
  {
    "StationID": 606535388084830303,
    "StationNameAR": "شبشير الحصه",
    "StationNameEN": "SHIBSHIR EL-HSA"
  },
  {
    "StationID": 606535388084830305,
    "StationNameAR": "صفط تراب",
    "StationNameEN": "SAFT TRAB"
  },
  {
    "StationID": 606535388084830306,
    "StationNameAR": "منيه شنتذا عياش",
    "StationNameEN": "MENYAT SHANTAZAH AYASH"
  },
  {
    "StationID": 606535390819516513,
    "StationNameAR": "منفلوط",
    "StationNameEN": "MANFALUT"
  },
  {
    "StationID": 606535388638478399,
    "StationNameAR": "ميت عنتر",
    "StationNameEN": "MEET ANTAR"
  },
  {
    "StationID": 606535388638478400,
    "StationNameAR": "شرنقاش",
    "StationNameEN": "SHERNKASH"
  },
  {
    "StationID": 606535388638478401,
    "StationNameAR": "الطويله",
    "StationNameEN": "EL TAWEELAH"
  },
  {
    "StationID": 606535388638478402,
    "StationNameAR": "ديسط",
    "StationNameEN": "DEESET"
  },
  {
    "StationID": 606535388638478403,
    "StationNameAR": "بطره",
    "StationNameEN": "BATRAH"
  },
  {
    "StationID": 606535388638478404,
    "StationNameAR": "ه.الحاج خليل",
    "StationNameEN": "EL HAG KHALIL"
  },
  {
    "StationID": 606535388638478405,
    "StationNameAR": "كفر الحطبه",
    "StationNameEN": "KAFT EL HATABAH"
  },
  {
    "StationID": 606535388638478406,
    "StationNameAR": "كفر الدبوس",
    "StationNameEN": "KAFR EL DABOOS"
  },
  {
    "StationID": 606535388638478408,
    "StationNameAR": "السعادوه",
    "StationNameEN": "EL SAADWAH"
  },
  {
    "StationID": 606535388638478409,
    "StationNameAR": "الصبريه",
    "StationNameEN": "EL SABRYA"
  },
  {
    "StationID": 606535388638478411,
    "StationNameAR": "جمصه",
    "StationNameEN": "GAMASA"
  },
  {
    "StationID": 606535388638478412,
    "StationNameAR": "السوالم",
    "StationNameEN": "EL SAWALEM"
  },
  {
    "StationID": 606535388638478413,
    "StationNameAR": "كفر سعد البلد",
    "StationNameEN": "KAFR SAAD EL BALAD"
  },
  {
    "StationID": 606535388638478414,
    "StationNameAR": "كفر سعد",
    "StationNameEN": "KAFRSAAD"
  },
  {
    "StationID": 606535388638478423,
    "StationNameAR": "الابراهيميه",
    "StationNameEN": "EL IBRAHIMYA"
  },
  {
    "StationID": 606535388638478424,
    "StationNameAR": "قطور",
    "StationNameEN": "KATOOR"
  },
  {
    "StationID": 606535388638478425,
    "StationNameAR": "حوين",
    "StationNameEN": "HEWEEN"
  },
  {
    "StationID": 606535388638478427,
    "StationNameAR": "عزبه جوده",
    "StationNameEN": "EZBAT GOUDA"
  },
  {
    "StationID": 606535388638478429,
    "StationNameAR": "قلين",
    "StationNameEN": "KELEEN"
  },
  {
    "StationID": 606535389179543611,
    "StationNameAR": "المنشاه الصغري",
    "StationNameEN": "EL MANSHYA EL SOGHRA"
  },
  {
    "StationID": 606535389179543612,
    "StationNameAR": "الحميديه",
    "StationNameEN": "EL HAMIDYAH"
  },
  {
    "StationID": 606535389179543614,
    "StationNameAR": "رزقه اماي",
    "StationNameEN": "REZKA AMAY"
  },
  {
    "StationID": 606535389179543615,
    "StationNameAR": "سخا",
    "StationNameEN": "SAKHA"
  },
  {
    "StationID": 606535389179543619,
    "StationNameAR": "دقميره",
    "StationNameEN": "DEKMEERA"
  },
  {
    "StationID": 606535389179543620,
    "StationNameAR": "السلام",
    "StationNameEN": "EL SALAM"
  },
  {
    "StationID": 606535389179543648,
    "StationNameAR": "كفر داود",
    "StationNameEN": "KAFR DAWOD"
  },
  {
    "StationID": 606535389733191733,
    "StationNameAR": "الجزيره الوسطانيه",
    "StationNameEN": "EL GEZIRA EL WESTANIA"
  },
  {
    "StationID": 606535389733191764,
    "StationNameAR": "مثلث سندوب",
    "StationNameEN": "MOTHALATH SANDOUB"
  },
  {
    "StationID": 606535389733191766,
    "StationNameAR": "الدنابيق",
    "StationNameEN": "EL DANABEEK"
  },
  {
    "StationID": 606535389733191767,
    "StationNameAR": "سلامون",
    "StationNameEN": "SLAMOUN"
  },
  {
    "StationID": 606535389733191768,
    "StationNameAR": "شها",
    "StationNameEN": "SHAHA"
  },
  {
    "StationID": 606535389733191769,
    "StationNameAR": "محـله دمنـه",
    "StationNameEN": "MAHALA DMNA"
  },
  {
    "StationID": 606535389733191770,
    "StationNameAR": "ميت ضافر",
    "StationNameEN": "MEET DAFER"
  },
  {
    "StationID": 606535389733191771,
    "StationNameAR": "الخشاشنه",
    "StationNameEN": "EL KHSHASHNA"
  },
  {
    "StationID": 606535389733191772,
    "StationNameAR": "دكرنس",
    "StationNameEN": "DEKERNES"
  },
  {
    "StationID": 606535389733191773,
    "StationNameAR": "ميت شــرف",
    "StationNameEN": "MEET SHARAF"
  },
  {
    "StationID": 606535389733191774,
    "StationNameAR": "اشمون الرمان",
    "StationNameEN": "ASHMON EL ROMAN"
  },
  {
    "StationID": 606535389733191775,
    "StationNameAR": "ميت الخولي",
    "StationNameEN": "MEET EL KHOLY"
  },
  {
    "StationID": 606535389733191776,
    "StationNameAR": "منشاه عاصم",
    "StationNameEN": "MANSHAT ASSEM"
  },
  {
    "StationID": 606535389733191777,
    "StationNameAR": "كفر علام",
    "StationNameEN": "KAFR ALLAM"
  },
  {
    "StationID": 606535389733191778,
    "StationNameAR": "الرياض",
    "StationNameEN": "EL RYAD"
  },
  {
    "StationID": 606535389733191779,
    "StationNameAR": "ميت حديد",
    "StationNameEN": "MEET HADEED"
  },
  {
    "StationID": 606535390282645555,
    "StationNameAR": "ميت سلسيل",
    "StationNameEN": "MEET SALSEEL"
  },
  {
    "StationID": 606535390282645556,
    "StationNameAR": "ميت مرجا سلسيل",
    "StationNameEN": "MEET MRGA SALSEEL"
  },
  {
    "StationID": 606535390282645557,
    "StationNameAR": "الجماليه",
    "StationNameEN": "EL GAMALIA"
  },
  {
    "StationID": 606535390282645558,
    "StationNameAR": "ميت خضير",
    "StationNameEN": "MEET KHODEIR"
  },
  {
    "StationID": 606535390282645559,
    "StationNameAR": "المنزله",
    "StationNameEN": "EL MANZALA"
  },
  {
    "StationID": 606535390282645561,
    "StationNameAR": "العصافره دقهليه",
    "StationNameEN": "EL ASAFRA DAQAHLIA"
  },
  {
    "StationID": 606535390282645562,
    "StationNameAR": "المطريه دقهليه",
    "StationNameEN": "EL MATTARIA DAQAHLIA"
  },
  {
    "StationID": 606535389733191748,
    "StationNameAR": "الروس",
    "StationNameEN": "EL ROUS"
  },
  {
    "StationID": 606535389733191749,
    "StationNameAR": "الناصريه",
    "StationNameEN": "EL NASERIA"
  },
  {
    "StationID": 606535389733191751,
    "StationNameAR": "العدوه",
    "StationNameEN": "EL ADWA"
  },
  {
    "StationID": 606535390282645563,
    "StationNameAR": "التفرع",
    "StationNameEN": "EL TFRA"
  },
  {
    "StationID": 606535390282645588,
    "StationNameAR": "المزغونه",
    "StationNameEN": "EL-MAZGHONA"
  },
  {
    "StationID": 606535390282645597,
    "StationNameAR": "الرقه",
    "StationNameEN": "EL-RIQA"
  },
  {
    "StationID": 606535390282645596,
    "StationNameAR": "القطوري",
    "StationNameEN": "EL-QATOORI"
  },
  {
    "StationID": 606535390282645600,
    "StationNameAR": "الواسطي",
    "StationNameEN": "EL-WASTY"
  },
  {
    "StationID": 606535390282645601,
    "StationNameAR": "قمن العروس",
    "StationNameEN": "QAMN EL-AROOS"
  },
  {
    "StationID": 606535390282645602,
    "StationNameAR": "بني حدير",
    "StationNameEN": "BANI HODEER"
  },
  {
    "StationID": 606535390282645603,
    "StationNameAR": "الميمون",
    "StationNameEN": "EL-MAIMOON"
  },
  {
    "StationID": 606535390819516466,
    "StationNameAR": "اشمنت",
    "StationNameEN": "ASHMANT"
  },
  {
    "StationID": 606535390819516467,
    "StationNameAR": "الزيتون قبلي",
    "StationNameEN": "EL-ZYTOON QIBLI"
  },
  {
    "StationID": 606535390819516468,
    "StationNameAR": "ناصر",
    "StationNameEN": "NASER"
  },
  {
    "StationID": 606535390819516469,
    "StationNameAR": "شريف باشا",
    "StationNameEN": "SHREEF BASHA"
  },
  {
    "StationID": 606535390282645592,
    "StationNameAR": "العياط",
    "StationNameEN": "EL-AIAT"
  },
  {
    "StationID": 606535390282645581,
    "StationNameAR": "الحوامديه",
    "StationNameEN": "EL-HAWAMDIA"
  },
  {
    "StationID": 606535390282645584,
    "StationNameAR": "المرازيق",
    "StationNameEN": "EL-MARAZIQ"
  },
  {
    "StationID": 606535389733191746,
    "StationNameAR": "كوم ابو راضي",
    "StationNameEN": "KOM ABU RADY"
  },
  {
    "StationID": 606535390282645594,
    "StationNameAR": "ميت القائد",
    "StationNameEN": "MEET EL-QAID"
  },
  {
    "StationID": 606535390819516471,
    "StationNameAR": "تزمنت",
    "StationNameEN": "TAZMONT"
  },
  {
    "StationID": 606535390819516473,
    "StationNameAR": "طحا البيشه",
    "StationNameEN": "TAHA EL-BEESHA"
  },
  {
    "StationID": 606535390819516474,
    "StationNameAR": "ببا",
    "StationNameEN": "BIBA"
  },
  {
    "StationID": 606535390819516475,
    "StationNameAR": "سدس",
    "StationNameEN": "SODS"
  },
  {
    "StationID": 606535390819516477,
    "StationNameAR": "الفشن",
    "StationNameEN": "EL-FASHN"
  },
  {
    "StationID": 606535390819516480,
    "StationNameAR": "ملاطيه",
    "StationNameEN": "MALATIA"
  },
  {
    "StationID": 606535390819516484,
    "StationNameAR": "بني مزار",
    "StationNameEN": "BANI MAZAR"
  },
  {
    "StationID": 606535390819516486,
    "StationNameAR": "مطاي",
    "StationNameEN": "MATAI"
  },
  {
    "StationID": 606535390819516487,
    "StationNameAR": "قلوصنا",
    "StationNameEN": "QLOWSNA"
  },
  {
    "StationID": 606535390819516488,
    "StationNameAR": "سمالوط",
    "StationNameEN": "SAMALUT"
  },
  {
    "StationID": 606535390819516494,
    "StationNameAR": "مصنع الغزل",
    "StationNameEN": "MASNA EL-GAZL"
  },
  {
    "StationID": 606535390819516495,
    "StationNameAR": "بني احمد",
    "StationNameEN": "BANI AHMED"
  },
  {
    "StationID": 606535390819516496,
    "StationNameAR": "منسافيس",
    "StationNameEN": "MINSAFEES"
  },
  {
    "StationID": 606535390819516497,
    "StationNameAR": "ابيوها",
    "StationNameEN": "ABIOHA"
  },
  {
    "StationID": 606535390819516498,
    "StationNameAR": "ابو قرقاص",
    "StationNameEN": "ABU QORGAS"
  },
  {
    "StationID": 606535390819516499,
    "StationNameAR": "اتليدم",
    "StationNameEN": "ATLEEDM"
  },
  {
    "StationID": 606535390819516500,
    "StationNameAR": "المحرص",
    "StationNameEN": "EL-MAHRASS"
  },
  {
    "StationID": 606535390819516501,
    "StationNameAR": "الروضه",
    "StationNameEN": "EL-RODA"
  },
  {
    "StationID": 606535390819516502,
    "StationNameAR": "ملوي",
    "StationNameEN": "MALLAWI"
  },
  {
    "StationID": 606535390819516503,
    "StationNameAR": "معصره ملوي",
    "StationNameEN": "MASSRA MALLAWI"
  },
  {
    "StationID": 606535390819516504,
    "StationNameAR": "تل العمارنه",
    "StationNameEN": "TAL EL-AMARNA"
  },
  {
    "StationID": 606535390819516505,
    "StationNameAR": "دير مواس",
    "StationNameEN": "DEER MWASS"
  },
  {
    "StationID": 606535390819516506,
    "StationNameAR": "الجرف",
    "StationNameEN": "EL-GARF"
  },
  {
    "StationID": 606535390819516507,
    "StationNameAR": "ديروط",
    "StationNameEN": "DAIRUT"
  },
  {
    "StationID": 606535390819516508,
    "StationNameAR": "صنبو",
    "StationNameEN": "SANBOW"
  },
  {
    "StationID": 606535390819516482,
    "StationNameAR": "ابا الوقف",
    "StationNameEN": "ABA EL-WAQF"
  },
  {
    "StationID": 606535393554202697,
    "StationNameAR": "منيه شبين القناطر",
    "StationNameEN": "MNYH SHEBIN AL KANATER"
  },
  {
    "StationID": 606535390819516481,
    "StationNameAR": "مغاغه",
    "StationNameEN": "MAGHAGHA"
  },
  {
    "StationID": 606535390819516510,
    "StationNameAR": "القوصيه",
    "StationNameEN": "EL-QOSIAH"
  },
  {
    "StationID": 606535390819516511,
    "StationNameAR": "بني قره",
    "StationNameEN": "BANI QORA"
  },
  {
    "StationID": 606535390819516512,
    "StationNameAR": "بني شقير",
    "StationNameEN": "BANI SHOQEER"
  },
  {
    "StationID": 606535390819516514,
    "StationNameAR": "الحواتكه",
    "StationNameEN": "EL-HWATKA"
  },
  {
    "StationID": 606535390819516515,
    "StationNameAR": "نجع سبع",
    "StationNameEN": "NAGA SABAH"
  },
  {
    "StationID": 606535391364775986,
    "StationNameAR": "بني حسين",
    "StationNameEN": "BANI HUSSIN"
  },
  {
    "StationID": 606535391364775987,
    "StationNameAR": "منقباد",
    "StationNameEN": "MANGBAD"
  },
  {
    "StationID": 606535391364775988,
    "StationNameAR": "الفوسفات",
    "StationNameEN": "EL-PHOSPHAT"
  },
  {
    "StationID": 606535391364775991,
    "StationNameAR": "المطيعه",
    "StationNameEN": "EL-MATIAA"
  },
  {
    "StationID": 606535391364775992,
    "StationNameAR": "باقور",
    "StationNameEN": "BAQOOR"
  },
  {
    "StationID": 606535391364775993,
    "StationNameAR": "ابو تيج",
    "StationNameEN": "ABU TIG"
  },
  {
    "StationID": 606535391364775994,
    "StationNameAR": "النخيله",
    "StationNameEN": "EL-NIKHEELA"
  },
  {
    "StationID": 606535391364775995,
    "StationNameAR": "صدفا",
    "StationNameEN": "SIDFA"
  },
  {
    "StationID": 606535391364775996,
    "StationNameAR": "اولاد الياس",
    "StationNameEN": "AWLAD ALYAS"
  },
  {
    "StationID": 606535391364776000,
    "StationNameAR": "بنجا",
    "StationNameEN": "BANGA"
  },
  {
    "StationID": 606535391364776002,
    "StationNameAR": "الصوامعه",
    "StationNameEN": "EL-SWAMAA"
  },
  {
    "StationID": 606535391364776003,
    "StationNameAR": "الشهيد السايح",
    "StationNameEN": "EL-SHAHEED EL-SAYEH"
  },
  {
    "StationID": 606535391364776004,
    "StationNameAR": "المراغه",
    "StationNameEN": "EL-MARAGHA"
  },
  {
    "StationID": 606535391364776005,
    "StationNameAR": "شندويل البلد",
    "StationNameEN": "SHANDWEEL EL-BALAD"
  },
  {
    "StationID": 606535391364776006,
    "StationNameAR": "جزيرة شندويل",
    "StationNameEN": "GEZEERAT SHANDWEEL"
  },
  {
    "StationID": 606535391364776007,
    "StationNameAR": "الحماديه",
    "StationNameEN": "EL HAMADIA"
  },
  {
    "StationID": 606535391364776009,
    "StationNameAR": "بلصفوره",
    "StationNameEN": "BILSAFORA"
  },
  {
    "StationID": 606535391364776010,
    "StationNameAR": "العيسويه",
    "StationNameEN": "EL-ESAWIA"
  },
  {
    "StationID": 606535391364776011,
    "StationNameAR": "المنشاه",
    "StationNameEN": "EL-MONSHAA"
  },
  {
    "StationID": 606535391364776016,
    "StationNameAR": "مزاتا غرب",
    "StationNameEN": "MIZATA GARB"
  },
  {
    "StationID": 606535391364775997,
    "StationNameAR": "طما",
    "StationNameEN": "TEMA"
  },
  {
    "StationID": 606535391364776001,
    "StationNameAR": "طهطا",
    "StationNameEN": "TAHTA"
  },
  {
    "StationID": 606535391364776018,
    "StationNameAR": "الساحل القبلي",
    "StationNameEN": "EL-SAHL-QIBLI"
  },
  {
    "StationID": 606535391364776019,
    "StationNameAR": "البلينا",
    "StationNameEN": "EL-BALYANA"
  },
  {
    "StationID": 606535391364776020,
    "StationNameAR": "بني حميل",
    "StationNameEN": "BANI GAMEEL"
  },
  {
    "StationID": 606535391364776021,
    "StationNameAR": "ابو شوشه",
    "StationNameEN": "ABU SHOSHA"
  },
  {
    "StationID": 606535391364776022,
    "StationNameAR": "سمهود",
    "StationNameEN": "SAMHOOD"
  },
  {
    "StationID": 606535391364776023,
    "StationNameAR": "ابوطشت",
    "StationNameEN": "ABU TISHT"
  },
  {
    "StationID": 606535391364776024,
    "StationNameAR": "رفاعه",
    "StationNameEN": "REEFAA"
  },
  {
    "StationID": 606535391364776025,
    "StationNameAR": "فرشوط",
    "StationNameEN": "FARSHUT"
  },
  {
    "StationID": 606535391364776026,
    "StationNameAR": "بهجوره",
    "StationNameEN": "BAHGOURA"
  },
  {
    "StationID": 606535391364776029,
    "StationNameAR": "السلميه قبلي",
    "StationNameEN": "EL-SALMIA QIBLI"
  },
  {
    "StationID": 606535391364776030,
    "StationNameAR": "الرحمانيه قبلي",
    "StationNameEN": "EL-RAHMANIA QIBLI"
  },
  {
    "StationID": 606535391364776031,
    "StationNameAR": "الياسينيه",
    "StationNameEN": "EL-YASENIA"
  },
  {
    "StationID": 606535391364776033,
    "StationNameAR": "دشنا",
    "StationNameEN": "DISHNA"
  },
  {
    "StationID": 606535391364776034,
    "StationNameAR": "المراشده",
    "StationNameEN": "EL-MARASHDA"
  },
  {
    "StationID": 606535391364776035,
    "StationNameAR": "السمطا",
    "StationNameEN": "EL-SAMTA"
  },
  {
    "StationID": 606535391914229811,
    "StationNameAR": "اولاد عمرو",
    "StationNameEN": "AWLAAD AMRO"
  },
  {
    "StationID": 606535391914229812,
    "StationNameAR": "القناويه",
    "StationNameEN": "EL-QENAWIA"
  },
  {
    "StationID": 606535391914229813,
    "StationNameAR": "المخادمه",
    "StationNameEN": "EL-MAKHADMA"
  },
  {
    "StationID": 606535391914229814,
    "StationNameAR": "الجزيريه",
    "StationNameEN": "EL-GIZEERIA"
  },
  {
    "StationID": 606535386352582722,
    "StationNameAR": "الزنكلون",
    "StationNameEN": "EL-ZNKALOON"
  },
  {
    "StationID": 606535391914229816,
    "StationNameAR": "الاشراف القبليه",
    "StationNameEN": "EL-ASHRAF EL-QIBLIA"
  },
  {
    "StationID": 606535391914229817,
    "StationNameAR": "الاشراف",
    "StationNameEN": "EL-ASHRAF"
  },
  {
    "StationID": 606535391914229818,
    "StationNameAR": "ابنود",
    "StationNameEN": "ABNOOD"
  },
  {
    "StationID": 606535391914229819,
    "StationNameAR": "البراهمه",
    "StationNameEN": "EL-BRAHMA"
  },
  {
    "StationID": 606535391914229820,
    "StationNameAR": "قفط",
    "StationNameEN": "QIFT"
  },
  {
    "StationID": 606535391914229821,
    "StationNameAR": "الكرتيه",
    "StationNameEN": "EL-KRTIA"
  },
  {
    "StationID": 606535391914229822,
    "StationNameAR": "قوص",
    "StationNameEN": "QUS"
  },
  {
    "StationID": 606535386931396666,
    "StationNameAR": "جنيفه",
    "StationNameEN": "GENEFAH"
  },
  {
    "StationID": 606535383991189595,
    "StationNameAR": "ابو حمص",
    "StationNameEN": "ABU HOMOSS"
  },
  {
    "StationID": 606535391914229823,
    "StationNameAR": "الشيخ عامر",
    "StationNameEN": "EL-SHEEKH AMMR"
  },
  {
    "StationID": 606535391914229824,
    "StationNameAR": "الشنهوريه",
    "StationNameEN": "EL-SHANHORIA"
  },
  {
    "StationID": 606535391914229825,
    "StationNameAR": "العيايشه",
    "StationNameEN": "ALIYAYSHA"
  },
  {
    "StationID": 606535391914229826,
    "StationNameAR": "خزام",
    "StationNameEN": "KHOZAM"
  },
  {
    "StationID": 606535391914229827,
    "StationNameAR": "الزيتيه",
    "StationNameEN": "ALZYTYH"
  },
  {
    "StationID": 606535391914229828,
    "StationNameAR": "الكرنك",
    "StationNameEN": "ALKRNK"
  },
  {
    "StationID": 606535391914229830,
    "StationNameAR": "الرضوانيه",
    "StationNameEN": "EL-RADWANIA"
  },
  {
    "StationID": 606535391914229831,
    "StationNameAR": "البغدادي",
    "StationNameEN": "EL-BAGDADI"
  },
  {
    "StationID": 606535391914229832,
    "StationNameAR": "الوحده",
    "StationNameEN": "EL-WIHDA"
  },
  {
    "StationID": 606535391914229833,
    "StationNameAR": "الطود",
    "StationNameEN": "EL-TAWD"
  },
  {
    "StationID": 606535391914229834,
    "StationNameAR": "ارمنت",
    "StationNameEN": "ARMNT"
  },
  {
    "StationID": 606535391914229835,
    "StationNameAR": "العديسات",
    "StationNameEN": "AL-ODISSAT"
  },
  {
    "StationID": 606535391914229836,
    "StationNameAR": "ه.نجع الجسور",
    "StationNameEN": "NAG EL-GOSOOR"
  },
  {
    "StationID": 606535391914229837,
    "StationNameAR": "الشغب",
    "StationNameEN": "AL-SHAGB"
  },
  {
    "StationID": 606535391914229838,
    "StationNameAR": "المعله",
    "StationNameEN": "EL-MAALA"
  },
  {
    "StationID": 606535391914229839,
    "StationNameAR": "نجع ابو سعيد",
    "StationNameEN": "NAG ABU SAID"
  },
  {
    "StationID": 606535391914229840,
    "StationNameAR": "المطاعنه",
    "StationNameEN": "AL-MATANA"
  },
  {
    "StationID": 606535391914229841,
    "StationNameAR": "الدير",
    "StationNameEN": "AL-DEER"
  },
  {
    "StationID": 606535391914229843,
    "StationNameAR": "الكلابيه",
    "StationNameEN": "EL-KLABIA"
  },
  {
    "StationID": 606535391914229844,
    "StationNameAR": "جزيره راجح",
    "StationNameEN": "GEZEERAT RAGH"
  },
  {
    "StationID": 606535386352582715,
    "StationNameAR": "شبلنجه",
    "StationNameEN": "SHABLANGA"
  },
  {
    "StationID": 606535391914229846,
    "StationNameAR": "السباعيه",
    "StationNameEN": "EL-SIBAIYA"
  },
  {
    "StationID": 606535391914229848,
    "StationNameAR": "الخوي",
    "StationNameEN": "EL-KHWI"
  },
  {
    "StationID": 606535391914229850,
    "StationNameAR": "الفوسفات  الجديدة",
    "StationNameEN": "EL-FOSFAT ELGADEDA"
  },
  {
    "StationID": 606535391914229851,
    "StationNameAR": "الكلح",
    "StationNameEN": "EL-KALH"
  },
  {
    "StationID": 606535391914229852,
    "StationNameAR": "الدومريه",
    "StationNameEN": "EL-DOMRIA"
  },
  {
    "StationID": 606535391914229853,
    "StationNameAR": "العطواني",
    "StationNameEN": "EL-ATWANI"
  },
  {
    "StationID": 606535391914229855,
    "StationNameAR": "الفوزه",
    "StationNameEN": "EL-FOOZA"
  },
  {
    "StationID": 606535391914229856,
    "StationNameAR": "الرديسيه",
    "StationNameEN": "AL-RODISAT"
  },
  {
    "StationID": 606535391914229857,
    "StationNameAR": "السراج",
    "StationNameEN": "EL-SORAG"
  },
  {
    "StationID": 606535391914229858,
    "StationNameAR": "الرمادي",
    "StationNameEN": "EL-RAMADI"
  },
  {
    "StationID": 606535391914229859,
    "StationNameAR": "جعفر الصادق",
    "StationNameEN": "GAAFR EL-SADIQ"
  },
  {
    "StationID": 606535392467877938,
    "StationNameAR": "سلوا",
    "StationNameEN": "SELWA"
  },
  {
    "StationID": 606535392467877939,
    "StationNameAR": "السيد سعيد",
    "StationNameEN": "EL-SAYED SAID"
  },
  {
    "StationID": 606535392467877941,
    "StationNameAR": "كلابشه",
    "StationNameEN": "KLABSHA"
  },
  {
    "StationID": 606535392467877942,
    "StationNameAR": "جبل السلسله",
    "StationNameEN": "GABL EL-SILSLA"
  },
  {
    "StationID": 606535392467877943,
    "StationNameAR": "الرغامه",
    "StationNameEN": "EL-RGHAMA"
  },
  {
    "StationID": 606535391914229854,
    "StationNameAR": "ادفو",
    "StationNameEN": "IDFU"
  },
  {
    "StationID": 606535392467877945,
    "StationNameAR": "الشطب البلد",
    "StationNameEN": "SHATB EL-BALAD"
  },
  {
    "StationID": 606535392467877946,
    "StationNameAR": "دراو",
    "StationNameEN": "DARAW"
  },
  {
    "StationID": 606535392467877947,
    "StationNameAR": "السلام النوبيه",
    "StationNameEN": "EL-SALAM EL-NOBIA"
  },
  {
    "StationID": 606535392467877949,
    "StationNameAR": "الجعافره",
    "StationNameEN": "EL-GAAFRA"
  },
  {
    "StationID": 606535392467877950,
    "StationNameAR": "الاعقاب",
    "StationNameEN": "EL-AAQAB"
  },
  {
    "StationID": 606535392467877951,
    "StationNameAR": "الاعقاب قبلي",
    "StationNameEN": "EL-AAQAB QIBLI"
  },
  {
    "StationID": 606535392467877952,
    "StationNameAR": "الخطاره",
    "StationNameEN": "EL-KHATARA"
  },
  {
    "StationID": 606535392467877953,
    "StationNameAR": "الشديدة",
    "StationNameEN": "EL-SHADEDA"
  },
  {
    "StationID": 606535392467877954,
    "StationNameAR": "ابو الريش قبلي",
    "StationNameEN": "ABU EL-REESH QIBLI"
  },
  {
    "StationID": 606535392467877957,
    "StationNameAR": "الشيخ هارون",
    "StationNameEN": "EL-SHIKH HAROON"
  },
  {
    "StationID": 606535392467877958,
    "StationNameAR": "كيما",
    "StationNameEN": "KEEMA"
  },
  {
    "StationID": 606535392467877959,
    "StationNameAR": "الصداقه",
    "StationNameEN": "EL-SADAQA"
  },
  {
    "StationID": 606535384603557977,
    "StationNameAR": "مرسي مطروح",
    "StationNameEN": "MARSA MATROUH"
  },
  {
    "StationID": 606535392467877948,
    "StationNameAR": "بلانه",
    "StationNameEN": "BLANA"
  },
  {
    "StationID": 606535392467877940,
    "StationNameAR": "كاجوج",
    "StationNameEN": "KAGOOG"
  },
  {
    "StationID": 606535384603557939,
    "StationNameAR": "محرم بك",
    "StationNameEN": "MUHARAM BEK"
  },
  {
    "StationID": 606535386352582714,
    "StationNameAR": "منيه السباع",
    "StationNameEN": "MEYET EL SEBAA"
  },
  {
    "StationID": 606535391914229829,
    "StationNameAR": "الاقصر",
    "StationNameEN": "LUXOR"
  },
  {
    "StationID": 606535388638478397,
    "StationNameAR": "المنصوره",
    "StationNameEN": "EL-MNSOURA"
  },
  {
    "StationID": 606535384603557959,
    "StationNameAR": "الحمام",
    "StationNameEN": "EL HAMAM"
  },
  {
    "StationID": 606535391914229842,
    "StationNameAR": "اسنا",
    "StationNameEN": "ISNA"
  },
  {
    "StationID": 606535391914229815,
    "StationNameAR": "قنا",
    "StationNameEN": "QENA"
  },
  {
    "StationID": 606535393008943171,
    "StationNameAR": "بلوك 3 الجزيريه",
    "StationNameEN": "BLOK 3"
  },
  {
    "StationID": 606535393008943202,
    "StationNameAR": "منشيه الجبل الاصفر",
    "StationNameEN": "MSNSHIA EL GABAL EL ASFAR"
  },
  {
    "StationID": 606535393008943203,
    "StationNameAR": "اللواء عبدالستار هـ",
    "StationNameEN": "EL LEWA ABD EL SATAR"
  },
  {
    "StationID": 606535393554202676,
    "StationNameAR": "برج العرب الجديدة",
    "StationNameEN": "BORG ELARAB ELGDEDA"
  },
  {
    "StationID": 606535389733191747,
    "StationNameAR": "كيلو973ر9 ابوراضي",
    "StationNameEN": "KILO 9.973 ABU RADY"
  },
  {
    "StationID": 606535389733191752,
    "StationNameAR": "عامريه الفيوم",
    "StationNameEN": "AMRYET EL FAYOUM"
  },
  {
    "StationID": 606535393554202684,
    "StationNameAR": "كيلو 64",
    "StationNameEN": "KILO 64"
  },
  {
    "StationID": 606535393554202685,
    "StationNameAR": "الشروق",
    "StationNameEN": "EL-SHEROK"
  },
  {
    "StationID": 606535393564202685,
    "StationNameAR": "العبور",
    "StationNameEN": "EL-OBOUR"
  },
  {
    "StationID": 606535393554202686,
    "StationNameAR": "دمـاريــس",
    "StationNameEN": "DAMAREES"
  },
  {
    "StationID": 606535393554202687,
    "StationNameAR": "جامعة الأزهر",
    "StationNameEN": "AZHAR UNIVERSITY"
  },
  {
    "StationID": 606535393554202688,
    "StationNameAR": "الطوناب   هـ",
    "StationNameEN": "EL-TONAB H"
  },
  {
    "StationID": 606535389733191742,
    "StationNameAR": "اوسيم",
    "StationNameEN": "OSEEM"
  },
  {
    "StationID": 606535393554202689,
    "StationNameAR": "الرتاج  هـ.",
    "StationNameEN": "EL-RETAG H"
  },
  {
    "StationID": 606535393554202695,
    "StationNameAR": "غزالة عبدون",
    "StationNameEN": "GAZALET ABDOON"
  },
  {
    "StationID": 606535393554202698,
    "StationNameAR": "العجرود الجديده",
    "StationNameEN": "NEW AGROUD"
  },
  {
    "StationID": 606535393554202699,
    "StationNameAR": "جامعة الزقازيق",
    "StationNameEN": "GAMAA ZAQAZEQ"
  },
  {
    "StationID": 606535393554202704,
    "StationNameAR": "الفردية",
    "StationNameEN": "EL-FARDIA"
  },
  {
    "StationID": 606535393554202705,
    "StationNameAR": "المشتل",
    "StationNameEN": "EL-MASHTAL"
  },
  {
    "StationID": 606535393554202706,
    "StationNameAR": "الخزان",
    "StationNameEN": "EL KHAZAN"
  },
  {
    "StationID": 606535389733191750,
    "StationNameAR": "سيلا",
    "StationNameEN": "SELA"
  },
  {
    "StationID": 606535390282645582,
    "StationNameAR": "البدرشين",
    "StationNameEN": "EL-BADRASHEEN"
  },
  {
    "StationID": 606535386931396659,
    "StationNameAR": "ابو سلطان",
    "StationNameEN": "ABU SULTAN"
  },
  {
    "StationID": 606535390282645590,
    "StationNameAR": "البليده",
    "StationNameEN": "EL-BLEEDA"
  },
  {
    "StationID": 606535390282645598,
    "StationNameAR": "اطواب",
    "StationNameEN": "ATWAB"
  },
  {
    "StationID": 606535383991189578,
    "StationNameAR": "بركه السبع",
    "StationNameEN": "BIRKAT EL-SABA"
  },
  {
    "StationID": 606535386352582717,
    "StationNameAR": "كوم حلين",
    "StationNameEN": "KOUM HELEEN"
  },
  {
    "StationID": 606535389179543610,
    "StationNameAR": "نشرت",
    "StationNameEN": "NASHRET"
  },
  {
    "StationID": 606535383991189571,
    "StationNameAR": "طوخ",
    "StationNameEN": "TUKH"
  },
  {
    "StationID": 606535388084830304,
    "StationNameAR": "محله روح",
    "StationNameEN": "MAHLET ROUH"
  },
  {
    "StationID": 606535383991189587,
    "StationNameAR": "ايتاي البارود",
    "StationNameEN": "ITAI EL-BARUD"
  },
  {
    "StationID": 606535385769574473,
    "StationNameAR": "ههيا",
    "StationNameEN": "HIHHYA"
  },
  {
    "StationID": 606535386352582720,
    "StationNameAR": "الجديده",
    "StationNameEN": "EL-GADIDA"
  },
  {
    "StationID": 606535388638478410,
    "StationNameAR": "راس الخليج",
    "StationNameEN": "RAS EL KHALIG"
  },
  {
    "StationID": 606535386352582721,
    "StationNameAR": "القراقره",
    "StationNameEN": "EL-KARAKRA"
  },
  {
    "StationID": 606535388638478418,
    "StationNameAR": "دمياط",
    "StationNameEN": "DAMIETTA"
  },
  {
    "StationID": 606535393554202675,
    "StationNameAR": "25يناير",
    "StationNameEN": "25 JANUARY"
  },
  {
    "StationID": 606535386352582727,
    "StationNameAR": "ابو حماد",
    "StationNameEN": "ABO HAMAD"
  },
  {
    "StationID": 606535383991189602,
    "StationNameAR": "سيدي جابر",
    "StationNameEN": "SIDI GABER"
  },
  {
    "StationID": 606535386352582735,
    "StationNameAR": "ابو صوير",
    "StationNameEN": "ABO SOWER"
  },
  {
    "StationID": 606535386352582744,
    "StationNameAR": "القنطره غرب",
    "StationNameEN": "ALQNTRH GHARB"
  },
  {
    "StationID": 606535386352582739,
    "StationNameAR": "الاسماعيليه",
    "StationNameEN": "EL ESMAILIYAH"
  },
  {
    "StationID": 606535385769574466,
    "StationNameAR": "بردين",
    "StationNameEN": "BERDEEN"
  },
  {
    "StationID": 606535385769574459,
    "StationNameAR": "مشتول",
    "StationNameEN": "MASHTOOL"
  },
  {
    "StationID": 606535388084830285,
    "StationNameAR": "ميت غمر",
    "StationNameEN": "MEET GHAAMR"
  },
  {
    "StationID": 606535388084830288,
    "StationNameAR": "تفـــــهنا الاشـــراف",
    "StationNameEN": "TAFAHNA EL ASHRAF"
  },
  {
    "StationID": 606535392467877960,
    "StationNameAR": "السد العالي",
    "StationNameEN": "EL-SAD EL-ALY"
  },
  {
    "StationID": 606535391914229849,
    "StationNameAR": "المحاميد",
    "StationNameEN": "EL-MAHAMEED"
  },
  {
    "StationID": 606535389179543625,
    "StationNameAR": "ابشان",
    "StationNameEN": "EBSHAN"
  },
  {
    "StationID": 606535386352582709,
    "StationNameAR": "الصالحيه",
    "StationNameEN": "EL SALHEIA"
  },
  {
    "StationID": 673618280706998260,
    "StationNameAR": "عدلي منصور",
    "StationNameEN": "ADLY MANSOUR"
  },
  {
    "StationID": 606535389733191731,
    "StationNameAR": "وردان",
    "StationNameEN": "WARDAN"
  },
  {
    "StationID": 606535389179543607,
    "StationNameAR": "سنهور",
    "StationNameEN": "SENHOUR"
  },
  {
    "StationID": 606535387493433428,
    "StationNameAR": "الحامول",
    "StationNameEN": "EL HAMOOL"
  },
  {
    "StationID": 606535385769574483,
    "StationNameAR": "برقين",
    "StationNameEN": "BARKEEN"
  },
  {
    "StationID": 606535383991189586,
    "StationNameAR": "التوفيقيه",
    "StationNameEN": "EL-TAWFIKIA"
  },
  {
    "StationID": 606535389733191744,
    "StationNameAR": "بشتيل البلد",
    "StationNameEN": "BASHTEEL EL BALAD"
  },
  {
    "StationID": 606535384603557967,
    "StationNameAR": "الضبعه",
    "StationNameEN": "EL DABAA"
  },
  {
    "StationID": 606535384603557963,
    "StationNameAR": "العلمين",
    "StationNameEN": "EL ALAMEIN"
  },
  {
    "StationID": 606535388638478415,
    "StationNameAR": "التوفيقيه البلد",
    "StationNameEN": "EL TAWFIKYA EL BALAD"
  },
  {
    "StationID": 606535389179543623,
    "StationNameAR": "الكراكات",
    "StationNameEN": "EL KARAKAT"
  },
  {
    "StationID": 606535389179543616,
    "StationNameAR": "كفر الشيخ",
    "StationNameEN": "KAFR EL SHEIKH"
  },
  {
    "StationID": 606535389179543621,
    "StationNameAR": "سيدي غازي",
    "StationNameEN": "SIDI GHAZY"
  },
  {
    "StationID": 606535389179543613,
    "StationNameAR": "محله موسي",
    "StationNameEN": "MAHLA MOUSSA"
  },
  {
    "StationID": 606535388638478426,
    "StationNameAR": "الشين",
    "StationNameEN": "EL SHEEN"
  },
  {
    "StationID": 606535389179543624,
    "StationNameAR": "كوم الحجنه",
    "StationNameEN": "KOM EL HAGNA"
  },
  {
    "StationID": 606535389179543627,
    "StationNameAR": "كفر الجرايده",
    "StationNameEN": "KAFR EL GARAYDAH"
  },
  {
    "StationID": 606535389179543628,
    "StationNameAR": "بهوت",
    "StationNameEN": "BAHOOT"
  },
  {
    "StationID": 606535389733191739,
    "StationNameAR": "المناشي",
    "StationNameEN": "EL MANASHY"
  },
  {
    "StationID": 606535390282645554,
    "StationNameAR": "الكردي",
    "StationNameEN": "EL KORDY"
  },
  {
    "StationID": 606535389179543622,
    "StationNameAR": "الكوم الطويل",
    "StationNameEN": "EL KOM EL TAWEEL"
  },
  {
    "StationID": 606535387493433436,
    "StationNameAR": "اشمون",
    "StationNameEN": "ASHMOON"
  },
  {
    "StationID": 606535386352582726,
    "StationNameAR": "الصوه",
    "StationNameEN": "EL-SOUA"
  },
  {
    "StationID": 606535385199149122,
    "StationNameAR": "المعموره",
    "StationNameEN": "EL MAMOURA"
  },
  {
    "StationID": 606535385199149147,
    "StationNameAR": "سيدي معروف",
    "StationNameEN": "SIDI MAAROF"
  },
  {
    "StationID": 606535385769574474,
    "StationNameAR": "شرشيمه",
    "StationNameEN": "SHERSHEMA"
  },
  {
    "StationID": 606535386352582707,
    "StationNameAR": "العزازي",
    "StationNameEN": "EL AZZAZY"
  },
  {
    "StationID": 606535385769574498,
    "StationNameAR": "اكياد",
    "StationNameEN": "AKYAD"
  },
  {
    "StationID": 606535385769574491,
    "StationNameAR": "الغابه",
    "StationNameEN": "EL GHABA"
  },
  {
    "StationID": 606535386352582718,
    "StationNameAR": "ميت يزيد",
    "StationNameEN": "MEET YAZED"
  },
  {
    "StationID": 606535386352582725,
    "StationNameAR": "صفط الحنه",
    "StationNameEN": "SAFT EL-HEENA"
  },
  {
    "StationID": 606535386352582728,
    "StationNameAR": "محجر ابو حماد",
    "StationNameEN": "MHGAR ABO HAMAD"
  },
  {
    "StationID": 606535386352582730,
    "StationNameAR": "البعالوه",
    "StationNameEN": "EL-BAALOU"
  },
  {
    "StationID": 606535385769574461,
    "StationNameAR": "انشاص",
    "StationNameEN": "ANSHAS"
  },
  {
    "StationID": 606535386352582732,
    "StationNameAR": "المحسمه",
    "StationNameEN": "EL-MHASMA"
  },
  {
    "StationID": 606535386352582750,
    "StationNameAR": "الرسوه",
    "StationNameEN": "EL-RSWA"
  },
  {
    "StationID": 606535386352582754,
    "StationNameAR": "عين غصين",
    "StationNameEN": "EIN GHASEN"
  },
  {
    "StationID": 606535386931396660,
    "StationNameAR": "السعيديه",
    "StationNameEN": "EL SAEEDYAH"
  },
  {
    "StationID": 606535386931396662,
    "StationNameAR": "فايد الجديده",
    "StationNameEN": "FAYED EL GEDIDAH"
  },
  {
    "StationID": 606535391364776012,
    "StationNameAR": "الاحايوه",
    "StationNameEN": "EL-AHIWA"
  },
  {
    "StationID": 606535391364776013,
    "StationNameAR": "العسيرات",
    "StationNameEN": "EL-OSIRAT"
  },
  {
    "StationID": 606535391364776014,
    "StationNameAR": "البندار",
    "StationNameEN": "EL-BENDAR"
  },
  {
    "StationID": 606535384603557957,
    "StationNameAR": "برج العرب",
    "StationNameEN": "BORG EL ARAB"
  },
  {
    "StationID": 606535383991189555,
    "StationNameAR": "الخانكه",
    "StationNameEN": "EL KHANKA"
  },
  {
    "StationID": 606535383991189557,
    "StationNameAR": "ابو زعبل البلد",
    "StationNameEN": "ABU ZAABAL"
  },
  {
    "StationID": 606535383991189558,
    "StationNameAR": "محاجر ابو زعبل",
    "StationNameEN": "MAHAGER ABU ZAABAL"
  },
  {
    "StationID": 606535389733191741,
    "StationNameAR": "برطس",
    "StationNameEN": "BORTOS"
  },
  {
    "StationID": 606535383991189570,
    "StationNameAR": "قها",
    "StationNameEN": "QAHA"
  },
  {
    "StationID": 606535388084830271,
    "StationNameAR": "كوم مازن",
    "StationNameEN": "KOM MAZEN"
  },
  {
    "StationID": 606535388084830307,
    "StationNameAR": "منشيه البكري",
    "StationNameEN": "MENSHEYET EL BAKRY"
  },
  {
    "StationID": 606535388638478387,
    "StationNameAR": "غزل المحله",
    "StationNameEN": "GHAZAL EL MAHATA"
  },
  {
    "StationID": 606535388638478388,
    "StationNameAR": "محله ابو علي القنطره",
    "StationNameEN": "MAHALAT ABU ALY EL QANTARA"
  },
  {
    "StationID": 606535388638478389,
    "StationNameAR": "الراهبين",
    "StationNameEN": "AL RAHBEEN"
  },
  {
    "StationID": 606535388638478392,
    "StationNameAR": "ميت خلف",
    "StationNameEN": "MEET KHALAF"
  },
  {
    "StationID": 606535388638478393,
    "StationNameAR": "ميت عساس",
    "StationNameEN": "MEET ASSAS"
  },
  {
    "StationID": 606535388638478394,
    "StationNameAR": "كفر العرب",
    "StationNameEN": "KAFR EL ARAB"
  },
  {
    "StationID": 606535388638478395,
    "StationNameAR": "ميت الكرما",
    "StationNameEN": "MEET EL KARMA"
  },
  {
    "StationID": 606535388638478398,
    "StationNameAR": "سماد طلخا",
    "StationNameEN": "SEMAD TALKHA"
  },
  {
    "StationID": 606535389179543602,
    "StationNameAR": "دسوق",
    "StationNameEN": "DESOUK"
  },
  {
    "StationID": 606535387493433425,
    "StationNameAR": "شبين الكوم الجديده",
    "StationNameEN": "SHEBEEN EL KOM EL GEDIDA"
  },
  {
    "StationID": 606535383991189561,
    "StationNameAR": "العليقات",
    "StationNameEN": "EL ALYQAT"
  },
  {
    "StationID": 606535384603557953,
    "StationNameAR": "ايكنجي مريوط",
    "StationNameEN": "KING MARRIOT"
  },
  {
    "StationID": 606535384603557954,
    "StationNameAR": "مريوط",
    "StationNameEN": "MARRIOT"
  },
  {
    "StationID": 606535384603557956,
    "StationNameAR": "بهيج",
    "StationNameEN": "BAHEEG"
  },
  {
    "StationID": 606535384603557958,
    "StationNameAR": "الغربانيات",
    "StationNameEN": "EL GHARBANIAT"
  },
  {
    "StationID": 606535385199149117,
    "StationNameAR": "سيدي بشر",
    "StationNameEN": "SIDI BESHR"
  },
  {
    "StationID": 606535385199149148,
    "StationNameAR": "فوه",
    "StationNameEN": "FOUH"
  },
  {
    "StationID": 606535387493433432,
    "StationNameAR": "شما",
    "StationNameEN": "SHMA"
  },
  {
    "StationID": 606535390819516479,
    "StationNameAR": "الفنت",
    "StationNameEN": "EL-FANT"
  },
  {
    "StationID": 606535390819516509,
    "StationNameAR": "فزاره",
    "StationNameEN": "FAZARA"
  },
  {
    "StationID": 606535391364776017,
    "StationNameAR": "برديس",
    "StationNameEN": "BARDEES"
  },
  {
    "StationID": 606535384603557947,
    "StationNameAR": "المتراس",
    "StationNameEN": "EL METRAS"
  },
  {
    "StationID": 606535388084830301,
    "StationNameAR": "بلكيم",
    "StationNameEN": "BALKIIM"
  },
  {
    "StationID": 606535388638478386,
    "StationNameAR": "المحله الكبري",
    "StationNameEN": "EL MAHALA EL KOBRA"
  },
  {
    "StationID": 606535389179543631,
    "StationNameAR": "المحفوظه",
    "StationNameEN": "EL MAHFOOZA"
  },
  {
    "StationID": 606535389179543633,
    "StationNameAR": "كفر الشناوي",
    "StationNameEN": "KAFR EL SHENAWY"
  },
  {
    "StationID": 606535389179543646,
    "StationNameAR": "مديريه التحرير",
    "StationNameEN": "MUDERET EL TAHRIR"
  },
  {
    "StationID": 606535389179543651,
    "StationNameAR": "الخطاطبه",
    "StationNameEN": "EL KHATATBA"
  },
  {
    "StationID": 673619743512461300,
    "StationNameAR": "بدر",
    "StationNameEN": "BAADR"
  },
  {
    "StationID": 606535389179543626,
    "StationNameAR": "بيلا",
    "StationNameEN": "BILAH"
  },
  {
    "StationID": 606535388638478421,
    "StationNameAR": "سجين",
    "StationNameEN": "SEGEEN"
  },
  {
    "StationID": 606535386931396658,
    "StationNameAR": "10 رمضان",
    "StationNameEN": "10 RAMADAN"
  },
  {
    "StationID": 606535390819516472,
    "StationNameAR": "طنسا",
    "StationNameEN": "TANSAA"
  },
  {
    "StationID": 606535389179543618,
    "StationNameAR": "المرابعين",
    "StationNameEN": "EL MERABEEN"
  },
  {
    "StationID": 606535386352582733,
    "StationNameAR": "الشهيد محمد البغدادى",
    "StationNameEN": "EL-SHAHEED MOHAMED EL-BGHDADY"
  },
  {
    "StationID": 606535386352582734,
    "StationNameAR": "ابو جريش",
    "StationNameEN": "ABU GRESH"
  },
  {
    "StationID": 606535386352582736,
    "StationNameAR": "الواصفيه",
    "StationNameEN": "EL-WASFIA"
  },
  {
    "StationID": 606535386352582737,
    "StationNameAR": "نفيشه",
    "StationNameEN": "NFISHA"
  },
  {
    "StationID": 606535386352582738,
    "StationNameAR": "معسكر الجلاء",
    "StationNameEN": "MASKAR EL-GALAA"
  },
  {
    "StationID": 606535386352582741,
    "StationNameAR": "الفردان",
    "StationNameEN": "EL-FRDAN"
  },
  {
    "StationID": 606535387493433424,
    "StationNameAR": "طنبدي",
    "StationNameEN": "TANYDI"
  },
  {
    "StationID": 606535387493433443,
    "StationNameAR": "القناطر الخيريه القديمه",
    "StationNameEN": "EL QANATER EL KHAYRYAH EL KADEEMAH"
  },
  {
    "StationID": 606535386931396691,
    "StationNameAR": "جبل عويبد",
    "StationNameEN": "GABAL OBEID"
  },
  {
    "StationID": 606535388084830287,
    "StationNameAR": "دنديط",
    "StationNameEN": "DENDEET"
  },
  {
    "StationID": 606535383991189556,
    "StationNameAR": "الطيران",
    "StationNameEN": "EL TAYARAN"
  },
  {
    "StationID": 606535389733191737,
    "StationNameAR": "نكلا",
    "StationNameEN": "NAKLA"
  }
];



const BASE_API_URL = 'https://localhost:7192/api/Trip/search/alltrips';

document.addEventListener('DOMContentLoaded', () => {
    const fromInput = document.getElementById('from-station-name');
    const toInput = document.getElementById('to-station-name');
    const stationsDatalist = document.getElementById('stations');
    const searchForm = document.getElementById('search-form');
    const searchMessage = document.getElementById('search-message'); 

    const fromIdHidden = document.getElementById('from-station-id');
    const toIdHidden = document.getElementById('to-station-id');



    function populateDatalist() {
        stationsDatalist.innerHTML = '';
        allStations.forEach(station => {
            const option = document.createElement('option');

            option.value = station.StationNameAR;
            stationsDatalist.appendChild(option);
        });
        console.log(`تم تجهيز ${allStations.length} محطة للإكمال التلقائي.`);
    }


    function updateHiddenId(inputElement, hiddenIdElement) {
        const stationName = inputElement.value.trim();

        const selectedStation = allStations.find(s => s.StationNameAR === stationName);

        if (selectedStation) {

            hiddenIdElement.value = selectedStation.StationID;
        } else {

            hiddenIdElement.value = '';
        }
    }


    populateDatalist();


    fromInput.addEventListener('change', () => {
        updateHiddenId(fromInput, fromIdHidden);
    });

    toInput.addEventListener('change', () => {
        updateHiddenId(toInput, toIdHidden);
    });



    searchForm.addEventListener('submit', async (e) => {
        e.preventDefault(); 
        

        updateHiddenId(fromInput, fromIdHidden);
        updateHiddenId(toInput, toIdHidden);
        
        const departureStationName = fromInput.value.trim();
        const arrivalStationName = toInput.value.trim();
        
        if (!fromIdHidden.value || !toIdHidden.value) {
            searchMessage.textContent = "يرجى اختيار محطة انطلاق ووصول صحيحة من القائمة.";
            return;
        }

        searchMessage.textContent = "جاري البحث عن الرحلات...";
        

        const encodedDeparture = encodeURIComponent(departureStationName);
        const encodedArrival = encodeURIComponent(arrivalStationName);

        const fullUrl = `${BASE_API_URL}?departureStationName=${encodedDeparture}&arrivalStationName=${encodedArrival}`;
        
        try {
            const response = await fetch(fullUrl, {
                method: 'GET',
   
            });
            
            const data = await response.json().catch(() => ({}));
            
            if (response.ok) {

                searchMessage.textContent = `تم العثور على ${data.length || 0} رحلة. جاري الانتقال...`;
                

                localStorage.setItem('searchResults', JSON.stringify(data));
                localStorage.setItem('searchFromStationName', departureStationName);
                localStorage.setItem('searchToStationName', arrivalStationName);


                window.location.href = `Search.html`;

            } else {

                const errorMessage = data.message || data.title || "فشل البحث. يرجى التحقق من المدخلات.";
                searchMessage.textContent = `خطأ: ${errorMessage}`;
                console.error('API Error:', response.status, data);
            }

        } catch (err) {

            searchMessage.textContent = "لا يمكن الاتصال بالخادم الآن. يرجى التأكد من تشغيل الـ API.";
            console.error('Network/Fetch Error:', err);
        }
    });
});