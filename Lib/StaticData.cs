﻿using HlidacStatu.Lib.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml.Linq;

namespace HlidacStatu.Lib
{

    public static class StaticData
    {
        [Serializable]
        public class FirmaName
        {
            public string Jmeno { get; set; }
            public string Koncovka { get; set; }
        }

        static object lockObj = new object();
        static bool initialized = false;


        public static System.Xml.Linq.XDocument DatoveSchranky = null;
        public static XNamespace DatoveSchrankyNS = null;
        public static string App_Data_Path = null;
        public static string Web_Root = null;
        public static string[] Mestske_Firmy = new string[] { };
        public static HashSet<string> VsechnyStatniMestskeFirmy = new HashSet<string>();

        public static List<string> CiziStaty = new List<string>();
        public static HashSet<string> Jmena = new HashSet<string>();
        public static HashSet<string> Prijmeni = new HashSet<string>();
        public static HashSet<string> TopJmena = new HashSet<string>();
        public static HashSet<string> TopPrijmeni = new HashSet<string>();

        public static HashSet<string> Urady_OVM = new HashSet<string>();

        public static Devmasters.Cache.V20.File.FileCache<Dictionary<string, FirmaName>> FirmyNazvy = null;
        //public static Devmasters.Cache.V20.File.FileCache<Dictionary<string, string>> FirmyNazvyAscii = null;
        public static System.Collections.Concurrent.ConcurrentDictionary<string, string[]> FirmyNazvyOnlyAscii = new System.Collections.Concurrent.ConcurrentDictionary<string, string[]>();

        public static Devmasters.Cache.V20.File.FileCache<IEnumerable<AnalysisCalculation.IcoSmlouvaMinMax>> FirmyCasovePodezreleZalozene = null;
        public static Devmasters.Cache.V20.File.FileCache<Dictionary<string, Analysis.BasicDataForSubject<List<Analysis.BasicData<string>>>>> UradyObchodujiciSCasovePodezrelymiFirmami = null;

        public static Devmasters.Cache.V20.File.FileCache<List<KeyValuePair<HlidacStatu.Lib.Data.Osoba, Analysis.BasicData<string>>>> SponzorisVazbouNaStat = null;

        public static Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaPolitiky> FirmySVazbamiNaPolitiky_aktualni_Cache = null;
        public static Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaPolitiky> FirmySVazbamiNaPolitiky_nedavne_Cache = null;
        public static Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaPolitiky> FirmySVazbamiNaPolitiky_vsechny_Cache = null;

        //public static Devmasters.Cache.V20.File.FileCache<string[]> SmlouvySPolitiky_nedavne_Cache = null;
        //public static Devmasters.Cache.V20.File.FileCache<string[]> SmlouvySPolitiky_aktualni_Cache = null;
        //public static Devmasters.Cache.V20.File.FileCache<string[]> SmlouvySPolitiky_vsechny_Cache = null;

        public static Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat> UradyObchodujiciSFirmami_s_vazbouNaPolitiky_aktualni_Cache = null;
        public static Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat> UradyObchodujiciSFirmami_s_vazbouNaPolitiky_nedavne_Cache = null;
        public static Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat> UradyObchodujiciSFirmami_s_vazbouNaPolitiky_vsechny_Cache = null;

        public static Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat> UradyObchodujiciSNespolehlivymiPlatciDPH_Cache = null;
        public static Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat> NespolehlivyPlatciDPH_obchodySurady_Cache = null;


        public static Devmasters.Cache.V20.BaseCache<IEnumerable<Firma>> MinisterstvaCache = null;
        public static Devmasters.Cache.V20.BaseCache<IEnumerable<Firma>> VysokeSkolyCache = null;
        public static Devmasters.Cache.V20.BaseCache<IEnumerable<Firma>> KrajskeUradyCache = null;
        public static Devmasters.Cache.V20.BaseCache<IEnumerable<Firma>> ManualChoosenCache = null;
        public static Devmasters.Cache.V20.BaseCache<IEnumerable<Firma>> StatutarniMestaAllCache = null;
        public static Devmasters.Cache.V20.BaseCache<IEnumerable<Firma>> PrahaManualCache = null;
        public static Devmasters.Cache.V20.BaseCache<IEnumerable<Firma>> OrganizacniSlozkyStatuCache = null;
        public static Dictionary<string, string[]> MestaPodleKraju = null;
        public static string[] ObceIII_DS = null;

        //public static Devmasters.Cache.V20.File.FileCache<Dictionary<string, QueryStatistic.StatData>> FirmyStatsCache = null;

        public static Devmasters.Cache.V20.LocalMemory.AutoUpdatedLocalMemoryCache<List<Lib.Data.Osoba>> Politici = null;
        public static Devmasters.Cache.V20.LocalMemory.AutoUpdatedLocalMemoryCache<List<Lib.Data.FirmaEvent>> SponzorujiciFirmy_Vsechny = null;
        public static Devmasters.Cache.V20.LocalMemory.AutoUpdatedLocalMemoryCache<List<Lib.Data.FirmaEvent>> SponzorujiciFirmy_Nedavne = null;

        public static Devmasters.Cache.V20.LocalMemory.LocalMemoryCache<List<double>> BasicStatisticData = null;

        public static Dictionary<string, string> CPVKody = new Dictionary<string, string>();

        public static Devmasters.Cache.V20.LocalMemory.AutoUpdatedLocalMemoryCache<Lib.Text.WordpressPost[]> LastBlogPosts = null;

        public static Dictionary<string, Lib.Data.Firma.ZiskyZtraty> ZiskyZtraty = null;

        public static Dictionary<string, HlidacStatu.Lib.Analysis.TemplatedQuery> Afery = new Dictionary<string, Analysis.TemplatedQuery>();

        public static Devmasters.Cache.V20.LocalMemory.LocalMemoryCache<Dictionary<string, Lib.Data.NespolehlivyPlatceDPH>> NespolehlivyPlatciDPH = null;

        //public static SingletonManagerWithSetup<Data.External.TwitterPublisher, Tweetinvi.Models.TwitterCredentials> TweetingManager = null;

        public static string[] HejtmaniOd2016 = new string[] {
            "jaroslava-jermanova",
            "ivana-straska",
            "josef-bernard",
            "jana-vildumetzova",
            "oldrich-bubenicek",
            "martin-puta",
            "jiri-stepan",
            "martin-netolicky",
            "jiri-behounek",
            "bohumil-simek",
            "ladislav-oklestek",
            "jiri-cunek",
            "ivo-vondrak",
            "adriana-krnacova",
            };
        public static string[] Poslanci2017Novacci = new string[] {
"adam-vojtech-1","alena-gajduskova","antonin-stanek","dan-tok","dana-balcarova-3","daniel-pawlas","dominik-feri","frantisek-elfmark-1",
"frantisek-kopriva-20","ilona-mauritzova","ivan-bartos","ivan-jac-4","ivana-nevludova","ivo-vondrak-2","jakub-michalek","jan-bauer",
"jan-cizinsky","jan-hrncir","jan-kobza-1","jan-kubik-9","jan-lipavsky","jan-posvar-3","jan-rehounek","jana-krutakova","jana-levova-7",
"jana-vildumetzova","jaroslav-dvorak-110","jaroslav-martinu","jiri-blaha-82","jiri-hlavaty","jiri-masek-50","jiri-strycek",
"jiri-ventruba","josef-belica-2","julius-spicak","karel-krejza","karla-marikova","karla-slechtova","katerina-valachova",
"klara-dostalova","lenka-kozlova-4","lubomir-volny-1","lucie--safrankova","lukas-barton","lukas-cernohorsky","lukas-kolarik-1",
"marek-vyborny","marian-bojko","martin-baxa","martin-jiranek-1","mikulas-ferjencik-1","mikulas-peksa","milan-hnilicka-4",
"miloslav-rozner","monika-jarosova-6","monika-oborna","milan-pour","olga-richterova","ondrej-profant","ondrej-vesely","patrik-nacher",
"pavel-juricek-4","pavel-ruzicka-19","pavel-stanek-1","pavel-zacek-18","petr-beitl-1","petr-dolinek","petr-sadovsky-4",
"petr-tresnak-12","radek-holomcik","radek-koten","radek-rozvoral","radek-zlesak","radovan-vich-1","robert-pelikan",
"stanislav-blaha","stanislav-fridrich-2","stanislav-juranek","tatana-mala-2","tereza-hythova","tomas-hanzel","tomas-martinek-10",
"tomas-vymazal-7","vaclav-klaus-2","veronika-vrecionova","vit-rakusan","vlastimil-valek","vojtech-pikal","zdenek-podal"
};

        public static string[] Poslanci2017Vsichni = new string[] {
"adam-kalous-1","adam-vojtech-1","alena-gajduskova","ales-juchelka", "alexander-cerny","andrea-babisova","andrea-brzobohata","andrej-babis",
"antonin-stanek","barbora-koranova-1","bohuslav-sobotka","bohuslav-svoboda","dan-tok","dana-balcarova-3","daniel-pawlas","david-kasal",
"david-prazak-5","david-stolpa","dominik-feri","eva-fialova-8","frantisek-elfmark-1","frantisek-kopriva-20","frantisek-petrtyl","frantisek-vacha",
"hana-aulicka-jirovcova","helena-langsadlova","helena-valkova","ilona-mauritzova","ivan-adamec","ivan-bartos","ivan-jac-4","ivana-nevludova",
"ivo-pojezny","ivo-vondrak-2","jakub-janda","jakub-michalek","jan-bartosek","jan-bauer","jan-birke","jan-chvojka","jan-cizinsky","jan-farsky",
"jan-hamacek","jan-hrncir","jan-kobza-1","jan-kubik-9","jan-lipavsky","jan-posvar-3","jan-rehounek","jan-richter","jan-schiller","jan-skopecek",
"jan-volny","jan-zahradnik","jana-cernochova","jana-krutakova","jana-levova-7","jana-pastuchova","jana-vildumetzova","jaroslav-bzoch-1",
"jaroslav-dvorak-110","jaroslav-faltynek","jaroslav-foldyna","jaroslav-holik","jaroslav-kytyr","jaroslav-martinu","jiri-behounek",
"jiri-blaha-82","jiri-dolejs","jiri-hlavaty","jiri-kohoutek-9","jiri-masek-50","jiri-mihola","jiri-strycek","jiri-valenta",
"jiri-ventruba","josef-belica-2","josef-hajek","josef-kott","julius-spicak","kamal-farhan","karel-krejza","karel-rais","karel-schwarzenberg",
"karel-turecek","karla-marikova","karla-slechtova","katerina-valachova","klara-dostalova","kveta-matusovska","ladislav-oklestek",
"lenka-drazilova-1","lenka-kozlova-4","leo-luzar","lubomir-spanel-1","lubomir-volny-1","lubomir-zaoralek","lucie--safrankova","lukas-barton",
"lukas-cernohorsky","lukas-kolarik-1","marcela-melkova","marek-benda","marek-novak-12","marek-vyborny","margita-balastikova","marian-bojko",
"marian-jurecka","marketa-adamova","martin-baxa","martin-jiranek-1","martin-kolovratnik","martin-stropnicky","michal-ratiborsky",
"mikulas-ferjencik-1","mikulas-peksa","milan-brazdil","milan-chovanec","milan-feranec","milan-hnilicka-4","miloslav-janulik","miloslav-rozner",
"miloslava-rutova","miloslava-vostra","miroslav-grebenicek","miroslav-kalousek","miroslava-nemcova","monika-jarosova-6","monika-oborna",
"olga-richterova","ondrej-benesik","ondrej-polansky","ondrej-profant","ondrej-vesely","patrik-nacher","pavel-belobradek","pavel-blazek",
"pavel-jelinek","pavel-juricek-4","pavel-kovacik","pavel-plzak","pavel-pustejovsky","pavel-ruzicka-19","pavel-stanek-1","pavel-zacek-18",
"pavla-golasowska","petr-beitl-1","petr-bendl","petr-dolinek","petr-fiala","petr-gazdik","petr-sadovsky-4","petr-tresnak-12","petr-vrana-2",
"milan-pour","premysl-malis-1","radek-holomcik","radek-koten","radek-rozvoral","radek-vondracek","radek-zlesak","radim-fiala",
"radka-maxova","radovan-vich-1","richard-brabec","robert-kralicek","robert-pelikan","roman-kubicek","roman-onderka","rostislav-vyzula",
"stanislav-berkovec","stanislav-blaha","stanislav-fridrich-2","stanislav-grospic","stanislav-juranek","tatana-mala-2","tereza-hythova",
"tomas-hanzel","tomas-kohoutek-7","tomas-martinek-10","tomas-vymazal-7","tomio-okamura","vaclav-klaus-2","vera-adamkova-1","vera-kovarova",
"vera-prochazkova-19","veronika-vrecionova","vit-kankovsky","vit-rakusan","vladimir-konicek","vlastimil-valek","vojtech-filip","vojtech-munzar",
"vojtech-pikal","zbynek-stanjura","zdenek-ondracek","zdenek-podal","zuzana-majerova-zahradnikova","zuzana-ozanova"
};

        public static string[] Vlada2017 = new string[] {
"andrej-babis","richard-brabec","dan-tok","","marta-novakova-29",
"jan-hamacek","jana-malacova","tomas-petricek","alena-schillerova","lubomir-metnar","adam-vojtech-1","petr-krcal",
"tatana-mala-2","robert-plaga","klara-dostalova","miroslav-toman","antonin-stanek"
    }; //Pridat Jan Kněžínek	


        static StaticData()
        {
            Init();
        }




        internal static void Init()
        {
            string appDataPath = Lib.Init.WebAppDataPath;

            lock (lockObj)
            {
                if (initialized)
                    return;

                HlidacStatu.Util.Consts.Logger.Info("Static data - Init start");
                //TweetingManager = new SingletonManagerWithSetup<Data.External.TwitterPublisher, Tweetinvi.Models.TwitterCredentials>();

                if (string.IsNullOrEmpty(appDataPath))
                {
                    throw new ArgumentNullException("appDataPath");
                }
                App_Data_Path = appDataPath;

                Web_Root = new System.IO.DirectoryInfo(appDataPath).Parent.FullName;

                HlidacStatu.Util.Consts.Logger.Info("Static data - loading cpv_cs");

                using (System.IO.StreamReader r = new StreamReader(App_Data_Path + "CPV_CS.txt"))
                {
                    var csv = new CsvHelper.CsvReader(r, new CsvHelper.Configuration.Configuration() { HasHeaderRecord = true, Delimiter = "\t" });
                    csv.Read(); csv.ReadHeader();
                    csv.Read();//skip second line
                    while (csv.Read())
                    {
                        string kod = csv.GetField<string>("Kód")?.Trim();
                        string text = csv.GetField<string>("Název")?.Trim();
                        if (!string.IsNullOrEmpty(kod) && !string.IsNullOrEmpty("text"))
                            CPVKody.Add(kod, text);
                    }

                }

                HlidacStatu.Util.Consts.Logger.Info("Static data - NespolehlivyPlatciDPH ");
                NespolehlivyPlatciDPH = new Devmasters.Cache.V20.LocalMemory.LocalMemoryCache<Dictionary<string, Lib.Data.NespolehlivyPlatceDPH>>
                    (TimeSpan.FromHours(12), "NespolehlivyPlatciDPH",
                    (o) =>
                    {
                        var data = Lib.Data.NespolehlivyPlatceDPH.GetAllFromDb();
                        if (data.Count == 0)
                        {
                            Lib.Data.NespolehlivyPlatceDPH.UpdateData();
                            data = Lib.Data.NespolehlivyPlatceDPH.GetAllFromDb();
                        }
                        return data;
                    });


                //HlidacStatu.Util.Consts.Logger.Info("Static data - FirmyStatsCache ");
                //FirmyStatsCache = new Devmasters.Cache.V20.File.FileCache<Dictionary<string, QueryStatistic.StatData>>
                //        (StaticData.App_Data_Path, TimeSpan.Zero, "FirmyStats",
                //            null //don't calculate new content. It's too time comsuming
                //        );

                HlidacStatu.Util.Consts.Logger.Info("Static data - Politici");
                Politici = new Devmasters.Cache.V20.LocalMemory.AutoUpdatedLocalMemoryCache<List<Lib.Data.Osoba>>(
                        TimeSpan.FromHours(36), (obj) =>
                        {
                            List<Osoba> osoby = null;

                            using (Lib.Data.DbEntities db = new DbEntities())
                            {
                                osoby = db.Osoba
                                    .AsNoTracking()
                                    .Where(m => m.Status > 0)
                                    .ToList();
                                //return osoby;
                                return osoby;

                            }

                        }
                    );

                HlidacStatu.Util.Consts.Logger.Info("Static data - SponzorujiciFirmy_Vsechny ");

                SponzorujiciFirmy_Vsechny = new Devmasters.Cache.V20.LocalMemory.AutoUpdatedLocalMemoryCache<List<Lib.Data.FirmaEvent>>(
                        TimeSpan.FromHours(3), (obj) =>
                        {
                            List<FirmaEvent> firmy = null;

                            using (Lib.Data.DbEntities db = new DbEntities())
                            {
                                firmy = db.FirmaEvent
                                    .AsNoTracking()
                                    .Where(m => m.Type == (int)FirmaEvent.Types.Sponzor)
                                    //.Select(m=>m.ICO)
                                    .ToList();

                                return firmy;

                            }

                        }
                    );

                HlidacStatu.Util.Consts.Logger.Info("Static data - SponzorujiciFirmy_nedavne");
                SponzorujiciFirmy_Nedavne = new Devmasters.Cache.V20.LocalMemory.AutoUpdatedLocalMemoryCache<List<Lib.Data.FirmaEvent>>(
                        TimeSpan.FromHours(3), (obj) =>
                        {
                            return StaticData.SponzorujiciFirmy_Vsechny.Get()
                                    .Where(m =>
                                        (m.DatumDo.HasValue && m.DatumDo.Value.Add(Relation.NedavnyVztahDelka) > DateTime.Now)
                                        ||
                                        (m.DatumOd.HasValue && m.DatumOd.Value.Add(Relation.NedavnyVztahDelka) > DateTime.Now)
                                    )
                                    .ToList();
                        }
                    );

                //if (!System.Diagnostics.Debugger.IsAttached)
                Politici.Get(); //force to load
                SponzorujiciFirmy_Vsechny.Get(); //force to load

                HlidacStatu.Util.Consts.Logger.Info("Static data - BasicStatisticData ");
                BasicStatisticData = new Devmasters.Cache.V20.LocalMemory.AutoUpdatedLocalMemoryCache<List<double>>(
                        TimeSpan.FromHours(6), (obj) =>
                        {
                            List<double> pol = new List<double>();
                            try
                            {

                                var res = HlidacStatu.Lib.ES.SearchTools.RawSearch("", 1, 0, platnyZaznam: 1, anyAggregation:
                                    new Nest.AggregationContainerDescriptor<HlidacStatu.Lib.Data.Smlouva>()
                                        .Sum("totalPrice", m => m
                                            .Field(ff => ff.CalculatedPriceWithVATinCZK)
                                    )
                                    );
                                var resNepl = HlidacStatu.Lib.ES.SearchTools.RawSearch("", 1, 0, platnyZaznam: 0, anyAggregation:
                                    new Nest.AggregationContainerDescriptor<HlidacStatu.Lib.Data.Smlouva>()
                                        .Sum("totalPrice", m => m
                                            .Field(ff => ff.CalculatedPriceWithVATinCZK)
                                    )
                                    );

                                long platnych = res.Total;
                                long neplatnych = resNepl.Total; ;
                                double celkemKc = 0;

                                celkemKc = ((Nest.ValueAggregate)res.Aggregations["totalPrice"]).Value.Value;

                                pol.Add(platnych);
                                pol.Add(neplatnych);
                                pol.Add(celkemKc);
                                return pol;
                            }
                            catch (Exception)
                            {
                                pol.Add(0);
                                pol.Add(0);
                                pol.Add(0);
                                return pol;
                            }
                        }

                    );


                string ds_ovm = App_Data_Path + "DS_OVM.xml";

                HlidacStatu.Util.Consts.Logger.Info("Static data - DatoveSchranky ");
                using (var xml = System.IO.File.OpenText(ds_ovm))
                {
                    DatoveSchranky = XDocument.Load(xml);
                }
                DatoveSchrankyNS = DatoveSchranky.Root.Name.Namespace;

                foreach (var ico in DatoveSchranky
                        .Descendants(StaticData.DatoveSchrankyNS + "Subjekt")
                        .Where(m => m.Element(StaticData.DatoveSchrankyNS + "TypDS")?.Value?.StartsWith("OVM") == true)
                        .Select(m => m.Element(StaticData.DatoveSchrankyNS + "ICO")?.Value ?? "")
                        .Where(i => !string.IsNullOrEmpty(i))
                        .Select(i=> HlidacStatu.Util.ParseTools.MerkIcoToICO(i)) 
                        )
                {
                    Urady_OVM.Add(ico);
                }

                HlidacStatu.Util.Consts.Logger.Info("Static data - SponzorisVazbouNaStat ");
                SponzorisVazbouNaStat = new Devmasters.Cache.V20.File.FileCache<List<KeyValuePair<Osoba, Analysis.BasicData<string>>>>(
                    StaticData.App_Data_Path, TimeSpan.Zero, "Sponzori_s_vazbouNaStat",
                    (o) =>
                    {
                        try
                        {

                            var vazbyNaPolitiky = StaticData.FirmySVazbamiNaPolitiky_nedavne_Cache.Get();
                            var allSponsors = HlidacStatu.Lib.Data.Sponsors.GetAllSponsors();
                            Dictionary<int, Analysis.BasicData<string>> sponzoriTmpCalc = new Dictionary<int, Analysis.BasicData<string>>();
                            foreach (var kvFirma in vazbyNaPolitiky.SoukromeFirmy)
                            {
                                var politiciVeFirme = kvFirma.Value;
                                if (politiciVeFirme.Any(p => allSponsors.ContainsKey(p)))
                                {
                                    var statForFirma = new Analysis.SubjectStatistic(kvFirma.Key);
                                    foreach (var p in politiciVeFirme.Distinct())
                                    {
                                        if (allSponsors.ContainsKey(p))
                                        {
                                            if (sponzoriTmpCalc.ContainsKey(p))
                                            {
                                                sponzoriTmpCalc[p].CelkemCena += statForFirma.BasicStatPerYear.Summary.CelkemCena;
                                                sponzoriTmpCalc[p].Pocet += statForFirma.BasicStatPerYear.Summary.Pocet;
                                            }
                                            else
                                            {
                                                sponzoriTmpCalc.Add(p, statForFirma.ToBasicData());
                                            }
                                        }

                                    }
                                }

                            }
                            return sponzoriTmpCalc.Select(m => new KeyValuePair<Osoba, Analysis.BasicData<string>>(allSponsors[m.Key], m.Value)).ToList();
                        }
                        catch (Exception e)
                        {
                            HlidacStatu.Util.Consts.Logger.Error("SponzorisVazbouNaStat", e);
                            throw;
                        }
                    }
                    );

                HlidacStatu.Util.Consts.Logger.Info("Static data - Mestske_Firmy ");
                Mestske_Firmy = System.IO.File
                    .ReadAllLines(StaticData.App_Data_Path + "mistni_firmy_ico.txt")
                    .Where(s => !string.IsNullOrEmpty(s.Trim()))
                    .ToArray();

                VsechnyStatniMestskeFirmy.UnionWith(Mestske_Firmy);
                XDocument xds = null;
                using (var xml = System.IO.File.OpenText(StaticData.App_Data_Path + @"datafile-seznam_ds_ovm.xml"))
                {
                    xds = XDocument.Load(xml);
                }
                var xds_ns = xds.Root.Name.Namespace;

                VsechnyStatniMestskeFirmy.UnionWith(xds
                   .Descendants(xds_ns + "box")
                   .Select(m => m.Element(xds_ns + "ico")?.Value ?? "")
                   .Where(i => !string.IsNullOrEmpty(i))
                   );

                VsechnyStatniMestskeFirmy.UnionWith(DatoveSchranky
                        .Descendants(StaticData.DatoveSchrankyNS + "Subjekt")
                        .Select(m => m.Element(StaticData.DatoveSchrankyNS + "ICO")?.Value ?? "")
                        .Where(i => !string.IsNullOrEmpty(i))
                   );


                //StatniMestskeFirmy.Add

                HlidacStatu.Util.Consts.Logger.Info("Static data - FirmySVazbamiNaPolitiky_*");
                FirmySVazbamiNaPolitiky_aktualni_Cache = new Devmasters.Cache.V20.File.FileCache<Lib.Data.AnalysisCalculation.VazbyFiremNaPolitiky>
                   (StaticData.App_Data_Path, TimeSpan.Zero, "FirmySVazbamiNaPolitiky_Aktualni",
                   (o) =>
                       {
                           return Lib.Data.AnalysisCalculation.LoadFirmySVazbamiNaPolitiky(Relation.AktualnostType.Aktualni, true);
                       });

                FirmySVazbamiNaPolitiky_nedavne_Cache = new Devmasters.Cache.V20.File.FileCache<Lib.Data.AnalysisCalculation.VazbyFiremNaPolitiky>
                   (StaticData.App_Data_Path, TimeSpan.Zero, "FirmySVazbamiNaPolitiky_Nedavne",
                   (o) =>
                   {
                       return Lib.Data.AnalysisCalculation.LoadFirmySVazbamiNaPolitiky(Relation.AktualnostType.Nedavny, true);
                   });

                FirmySVazbamiNaPolitiky_vsechny_Cache = new Devmasters.Cache.V20.File.FileCache<Lib.Data.AnalysisCalculation.VazbyFiremNaPolitiky>
                   (StaticData.App_Data_Path, TimeSpan.Zero, "FirmySVazbamiNaPolitiky_Vsechny",
                   (o) =>
                   {
                       return Lib.Data.AnalysisCalculation.LoadFirmySVazbamiNaPolitiky(Relation.AktualnostType.Libovolny, true);
                   });


                FirmyCasovePodezreleZalozene = new Devmasters.Cache.V20.File.FileCache<IEnumerable<AnalysisCalculation.IcoSmlouvaMinMax>>
                   (StaticData.App_Data_Path, TimeSpan.Zero, "FirmyCasovePodezreleZalozene",
                   (o) =>
                   {
                       return Lib.Data.AnalysisCalculation.GetFirmyCasovePodezreleZalozene();
                   });

                UradyObchodujiciSCasovePodezrelymiFirmami = new Devmasters.Cache.V20.File.FileCache<Dictionary<string, Analysis.BasicDataForSubject<List<Analysis.BasicData<string>>>>>
                   (StaticData.App_Data_Path, TimeSpan.Zero, "UradyObchodujiciSCasovePodezrelymiFirmami",
                   (o) =>
                   {
                       return Lib.Data.AnalysisCalculation.GetUradyObchodujiciSCasovePodezrelymiFirmami();
                   });

                //SmlouvySPolitiky_nedavne_Cache = new Devmasters.Cache.V20.File.FileCache<string[]>
                //    (StaticData.App_Data_Path, TimeSpan.Zero, "SmlouvySPolitiky_nedavne",
                //    (o) =>
                //    {
                //        return Lib.Data.AnalysisCalculation.SmlouvyIdSPolitiky(FirmySVazbamiNaPolitiky_nedavne_Cache.Get().SoukromeFirmy, SponzorujiciFirmy_Nedavne.Get(), true);
                //    });
                //SmlouvySPolitiky_aktualni_Cache = new Devmasters.Cache.V20.File.FileCache<string[]>
                //    (StaticData.App_Data_Path, TimeSpan.Zero, "SmlouvySPolitiky_aktualni",
                //    (o) =>
                //    {
                //        return Lib.Data.AnalysisCalculation.SmlouvyIdSPolitiky(FirmySVazbamiNaPolitiky_aktualni_Cache.Get().SoukromeFirmy, SponzorujiciFirmy_Nedavne.Get(), true);
                //    });
                //SmlouvySPolitiky_vsechny_Cache = new Devmasters.Cache.V20.File.FileCache<string[]>
                //    (StaticData.App_Data_Path, TimeSpan.Zero, "SmlouvySPolitiky_vsechny",
                //    (o) =>
                //    {
                //        return Lib.Data.AnalysisCalculation.SmlouvyIdSPolitiky(FirmySVazbamiNaPolitiky_vsechny_Cache.Get().SoukromeFirmy, SponzorujiciFirmy_Vsechny.Get(), true);
                //    });

                HlidacStatu.Util.Consts.Logger.Info("Static data - UradyObchodujiciSFirmami_s_vazbouNaPolitiky_*");
                UradyObchodujiciSFirmami_s_vazbouNaPolitiky_aktualni_Cache = new Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat>
                    (StaticData.App_Data_Path, TimeSpan.Zero, "UradyObchodujiciSFirmami_s_vazbouNaPolitiky_aktualni",
                    (o) =>
                    {
                        return AnalysisCalculation.UradyObchodujiciSFirmami_s_vazbouNaPolitiky(Relation.AktualnostType.Aktualni, true);
                    }
                    );
                UradyObchodujiciSFirmami_s_vazbouNaPolitiky_nedavne_Cache = new Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat>
                    (StaticData.App_Data_Path, TimeSpan.Zero, "UradyObchodujiciSFirmami_s_vazbouNaPolitiky_nedavne",
                    (o) =>
                    {
                        return AnalysisCalculation.UradyObchodujiciSFirmami_s_vazbouNaPolitiky(Relation.AktualnostType.Nedavny, true);
                    }
                    );
                UradyObchodujiciSFirmami_s_vazbouNaPolitiky_vsechny_Cache = new Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat>
                    (StaticData.App_Data_Path, TimeSpan.Zero, "UradyObchodujiciSFirmami_s_vazbouNaPolitiky_vsechny",
                    (o) =>
                    {
                        return AnalysisCalculation.UradyObchodujiciSFirmami_s_vazbouNaPolitiky(Relation.AktualnostType.Libovolny, true);
                    }
                    );

                HlidacStatu.Util.Consts.Logger.Info("Static data - UradyObchodujiciSNespolehlivymiPlatciDPH_Cache*");
                UradyObchodujiciSNespolehlivymiPlatciDPH_Cache = new Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat>
                    (StaticData.App_Data_Path, TimeSpan.Zero, "UradyObchodujiciSNespolehlivymiPlatciDPH",
                    (o) =>
                    {
                        return new AnalysisCalculation.VazbyFiremNaUradyStat(); //refresh from task
                    }
                    );
                NespolehlivyPlatciDPH_obchodySurady_Cache = new Devmasters.Cache.V20.File.FileCache<AnalysisCalculation.VazbyFiremNaUradyStat>
                    (StaticData.App_Data_Path, TimeSpan.Zero, "NespolehlivyPlatciDPH_obchodySurady",
                    (o) =>
                    {
                        return new AnalysisCalculation.VazbyFiremNaUradyStat(); //refresh from task
                    }
                    );

                //pravni forma : http://wwwinfo.mfcr.cz/ares/aresPrFor.html.cz

                HlidacStatu.Util.Consts.Logger.Info("Static data - MinisterstvaCache");
                MinisterstvaCache = new Devmasters.Cache.V20
                    .LocalMemory.LocalMemoryCache<IEnumerable<Firma>>(TimeSpan.FromHours(24), "StatData.Ministerstva",
                            (o) =>
                            {
                                return StaticData.DatoveSchranky
                                    .Descendants(StaticData.DatoveSchrankyNS + "Subjekt")
                                    .Where(m => m.Element(StaticData.DatoveSchrankyNS + "TypSubjektu")?.Attribute("id").Value == "4")
                                    .OrderBy(m => m.Element(StaticData.DatoveSchrankyNS + "Nazev").Value)
                                    .Select(m => m.Element(StaticData.DatoveSchrankyNS + "IdDS").Value)
                                    .Select(ds => Firmy.GetByDS(ds))
                                    .ToArray()
                                    ;
                            });

                VysokeSkolyCache = new Devmasters.Cache.V20
                    .LocalMemory.LocalMemoryCache<IEnumerable<Firma>>(TimeSpan.FromHours(24), "StatData.VysokeSkoly",
                        (o) =>
                        {
                            string[] icos = new string[] { "61384984","60461446","60460709","68407700","62156462","60076658","00216224","62156489","61988987","47813059","46747885","62690094","44555601","00216208",
                                "61989592","00216275","70883521","62157124","61989100","61384399","60461373","71226401","75081431","60461071","00216305","49777513","48135445"
                            };
                            //string[] ds = new string[] { "hkraife" };

                            return icos.Select(i => Firmy.Get(i))
                                .OrderBy(or => or.Jmeno);
                        });


                KrajskeUradyCache = new Devmasters.Cache.V20
                    .LocalMemory.LocalMemoryCache<IEnumerable<Firma>>(TimeSpan.FromHours(24), "StatData.KrajskeUrady",
                        (o) =>
                        {
                            return StaticData.DatoveSchranky
                                .Descendants(StaticData.DatoveSchrankyNS + "Subjekt")
                                .Where(m => m.Element(StaticData.DatoveSchrankyNS + "TypSubjektu")?.Attribute("id").Value == "3")
                                .OrderBy(m => m.Element(StaticData.DatoveSchrankyNS + "Nazev").Value)
                                .Select(m => m.Element(StaticData.DatoveSchrankyNS + "IdDS").Value)
                                .Select(ds => Firmy.GetByDS(ds));

                        });

                ManualChoosenCache = new Devmasters.Cache.V20
                    .LocalMemory.LocalMemoryCache<IEnumerable<Firma>>(TimeSpan.FromHours(24), "StatData.ManualChoosen",
                        (o) =>
                        {
                            string[] icos = new string[] { "72054506", "47114983", "61459445", "44848943", "69797111",
                                    "25291581", "41197518", "70890021", "47672234", "28244532", "00020729",
                                    "01312774", "70994234", "60498021", "63080249", "61388971","48136450","66000769",
                                    //krajske silnicni spravy
                                    "70947023","00090450","00064785","00073679","70971641","70932581","00065374","00074870",
                                    "00075779","00066621","00085031","72053119","00075957","00075477","00066001","00076520","00080837"
                            };
                            string[] ds = new string[]{ "trfaa33", "rnaadje", "weeab8c", "p9iwj4f", "zjq4rhz", "e8jcfsn",
                                "4iqaa3x", "yypyq58", "5smaetu", "gn5rgc9", "wwjaa4f", "kccaa9t",
                                "ag5uunk", "hkrkpwn" };

                            return icos.Select(i => Firmy.Get(i))
                                   .Union(ds.Select(d => Firmy.GetByDS(d)))
                                    .OrderBy(or => or.Jmeno);
                        });


                var obceIII = StaticData.DatoveSchranky
                       .Descendants(StaticData.DatoveSchrankyNS + "Subjekt")
                       .Where(m => m.Element(StaticData.DatoveSchrankyNS + "TypSubjektu")?.Attribute("id").Value == "7")
                       .Where(m => m.Element(StaticData.DatoveSchrankyNS + "PrimarniOvm")?.Value == "Ano");

                MestaPodleKraju = StaticData.DatoveSchranky
                    .Descendants(DatoveSchrankyNS + "Subjekt")
                    .Where(m => m.Element(DatoveSchrankyNS + "TypSubjektu")?.Attribute("id").Value == "8")
                    .Where(m => m.Element(DatoveSchrankyNS + "PrimarniOvm")?.Value == "Ano")
                    .Union(obceIII)
                    .GroupBy(k => k.Element(DatoveSchrankyNS + "AdresaUradu")?.Element(DatoveSchrankyNS + "KrajNazev")?.Value ?? "Královéhradecký" //chybejici u Dvora kraloveho
                                                                                                                                                   //, v => new { DS = v.Element(DatoveSchrankyNS + "IdDS").Value, Nazev = v.Element(DatoveSchrankyNS + "Nazev").Value }
                            , v => v.Element(DatoveSchrankyNS + "IdDS").Value
                            )
                    .ToDictionary(k => k.Key, v => v.ToArray());


                ObceIII_DS = obceIII
                       .OrderBy(m => m.Element(StaticData.DatoveSchrankyNS + "Nazev").Value)
                       .Select(m => m.Element(StaticData.DatoveSchrankyNS + "IdDS").Value)
                       .ToArray();

                StatutarniMestaAllCache = new Devmasters.Cache.V20
                    .LocalMemory.LocalMemoryCache<IEnumerable<Firma>>(TimeSpan.FromHours(24), "StatData.StatutarniMestaAll",
                        (o) =>
                        {
                            return StaticData.DatoveSchranky
                        .Descendants(StaticData.DatoveSchrankyNS + "Subjekt")
                        .Where(m => m.Element(StaticData.DatoveSchrankyNS + "TypSubjektu")?.Attribute("id").Value == "8")
                        .Where(m => m.Element(StaticData.DatoveSchrankyNS + "PrimarniOvm")?.Value == "Ano")
                        .OrderBy(m => m.Element(StaticData.DatoveSchrankyNS + "Nazev").Value)
                        .Select(m => m.Element(StaticData.DatoveSchrankyNS + "IdDS").Value)
                        .Union(ObceIII_DS)
                        .Select(d => Firmy.GetByDS(d))
                        .OrderBy(or => or.Jmeno);

                        });



                PrahaManualCache = new Devmasters.Cache.V20
                    .LocalMemory.LocalMemoryCache<IEnumerable<Firma>>(TimeSpan.FromHours(24), "StatData.PrahaManual",
                        (o) =>
                        {
                            var ds = new string[] { "48ia97h", "ktdeucu" };
                            return ds
                                .Select(d => Firmy.GetByDS(d))
                                .OrderBy(or => or.Jmeno);

                        });

                OrganizacniSlozkyStatuCache = new Devmasters.Cache.V20
                    .LocalMemory.LocalMemoryCache<IEnumerable<Firma>>(TimeSpan.FromHours(24), "StatData.OrganizacniSlozkyStatu",
                        (o) =>
                        {
                            return StaticData.DatoveSchranky
                                .Descendants(StaticData.DatoveSchrankyNS + "Subjekt")
                                .Where(m => m.Element(StaticData.DatoveSchrankyNS + "TypSubjektu")?.Attribute("id").Value == "11")
                                .Where(m => m.Element(StaticData.DatoveSchrankyNS + "PravniForma")?.Attribute("type").Value == "325")
                                .Where(m => m.Element(StaticData.DatoveSchrankyNS + "Nazev") != null)
                                .Where(m => m.Element(StaticData.DatoveSchrankyNS + "PrimarniOvm")?.Value == "Ano")
                                .OrderBy(m => m.Element(StaticData.DatoveSchrankyNS + "Nazev").Value)
                                .Select(m => m.Element(StaticData.DatoveSchrankyNS + "IdDS").Value)
                                .Select(d => Firmy.GetByDS(d))
                                .OrderBy(or => or.Jmeno);
                        });

                List<string> cizistaty = new List<string>();
                foreach (var line in System.IO.File.ReadAllLines(App_Data_Path + "staty.txt"))
                {
                    cizistaty.Add(Devmasters.Core.TextUtil.RemoveDiacritics(line.ToLower().Trim()));
                }
                CiziStaty = cizistaty.Where(m => !string.IsNullOrEmpty(m)).Distinct().ToList();


                Jmena = new HashSet<string>(System.IO.File.ReadAllLines(App_Data_Path + "jmena.txt")
                        .Select(m => Devmasters.Core.TextUtil.RemoveDiacritics(m.ToLower().Trim()))
                        .Distinct());


                Prijmeni = new HashSet<string>(System.IO.File.ReadAllLines(App_Data_Path + "prijmeni.txt")
                        .Select(m => Devmasters.Core.TextUtil.RemoveDiacritics(m.ToLower().Trim()))
                        .Distinct());

                TopJmena = new HashSet<string>(System.IO.File.ReadAllLines(App_Data_Path + "topjmena.txt")
                        .Select(m => Devmasters.Core.TextUtil.RemoveDiacritics(m.ToLower().Trim()))
                        .Distinct());
                TopPrijmeni = new HashSet<string>(System.IO.File.ReadAllLines(App_Data_Path + "topprijmeni.txt")
                        .Select(m => Devmasters.Core.TextUtil.RemoveDiacritics(m.ToLower().Trim()))
                        .Distinct());


                HlidacStatu.Util.Consts.Logger.Info("Static data - FirmyNazvy");

                FirmyNazvy = new Devmasters.Cache.V20.File.FileCache<Dictionary<string, FirmaName>>(
                    StaticData.App_Data_Path, TimeSpan.Zero, "FirmyNazvy",
                    (obj) =>
                        {
                            Dictionary<string, FirmaName> ff = new Dictionary<string, FirmaName>();

                            string cnnStr = Devmasters.Core.Util.Config.GetConfigValue("CnnString");
                            using (Devmasters.Core.PersistLib p = new Devmasters.Core.PersistLib())
                            {

                                var reader = p.ExecuteReader(cnnStr, CommandType.Text, "select ico, jmeno from firma", null);
                                int num = 0;
                                while (reader.Read())
                                {
                                    string ico = reader.GetString(0).Trim();
                                    string name = reader.GetString(1).Trim();
                                    if (Devmasters.Core.TextUtil.IsNumeric(ico))
                                    {
                                        num++;
                                        string koncovka;
                                        string jmeno = Firma.JmenoBezKoncovkyFull(name, out koncovka);
                                        //firmy.Add(ico, name);
                                        if (!ff.ContainsKey(ico))
                                            ff.Add(ico,
                                                new FirmaName()
                                                {
                                                    Jmeno = jmeno,
                                                    Koncovka = koncovka
                                                }
                                            );
                                    }
                                }

                            }
                            return ff;
                        }
                    );



                HlidacStatu.Util.Consts.Logger.Info("Static data - ZiskyZtraty");
                ZiskyZtraty = new Dictionary<string, Lib.Data.Firma.ZiskyZtraty>(5000);
                foreach (var line in System.IO.File.ReadAllLines(App_Data_Path + "ZiskyZtraty.tab"))
                {
                    string[] cols = line.Split('\t');//rok \t ico \t naklady \t vynosy

                    ZiskyZtraty.Add(cols[1], new Firma.ZiskyZtraty()
                    {
                        Ico = cols[1],
                        Rok = int.Parse(cols[0]),
                        Naklad = double.Parse(cols[2]),
                        Vynos = double.Parse(cols[3])
                    });

                }

                HlidacStatu.Util.Consts.Logger.Info("Static data - LastBlogPosts");
                LastBlogPosts = new Devmasters.Cache.V20.LocalMemory.AutoUpdatedLocalMemoryCache<Text.WordpressPost[]>(
                    TimeSpan.FromHours(3), (obj) =>
                        {
                            try
                            {
                                using (WebClient webClient = new WebClient())
                                {
                                    string blogUrl = @"https://www.hlidacstatu.cz/texty/wp-json/wp/v2/posts";
                                    string response = webClient.DownloadString(blogUrl);

                                    Lib.Text.WordpressPost[] posts = Newtonsoft.Json.JsonConvert.DeserializeObject<Lib.Text.WordpressPost[]>(response);

                                    return posts;
                                }

                            }
                            catch (Exception)
                            {
                                return new Lib.Text.WordpressPost[] { };
                            }
                        }
                    );


                //AFERY
                Afery.Add("parlamentnilisty", new HlidacStatu.Lib.Analysis.TemplatedQuery()
                {
                    Text = "Inzerce na Parlamentních listech",
                    Description = "Které úřady inzerují na serveru Parlamentní listy? Smlouvy s inzercí na médiích Our Media a.s. (vydavatel Parlamentních listů) anebo s mediaplánem přímo pro PL. Částky ze smluv jsou orientační a mohou obsahovat objednávky i na jiná média.",
                    Query = "\"OUR MEDIA\" OR \"Parlamentní listy\" OR \"Parlamentnilisty.cz\" OR ico:28876890 OR \"KrajskeListy.cz\" OR \"Krajské listy\" OR \"Prvnizpravy.cz\" OR icoPrijemce:24214868",
                    UrlTemplate = "/HledatSmlouvy?Q={0}",
                    Links = new HlidacStatu.Lib.Analysis.TemplatedQuery.AHref[]{
                        new HlidacStatu.Lib.Analysis.TemplatedQuery.AHref("https://web.archive.org/web/20170102215202/http://mediahub.cz/komunikace-35809/clanky-v-parlamentnich-listech-si-plati-ministri-i-hejtmani-celkove-castky-jdou-do-milionu-inzerce-vypada-jako-redakcni-clanky-1058528",
                        "Články v Parlamentních listech si platí ministři i hejtmani. Celkové částky jdou do milionů. Inzerce vypadá jako redakční články"),
                        new HlidacStatu.Lib.Analysis.TemplatedQuery.AHref("https://denikn.cz/39674/valentuv-server-zautocil-na-transparency-hned-pote-co-ho-narkla-ze-stretu-zajmu-nahoda-rika-senator/",
                        "Valentův server zaútočil na Transparency hned poté, co ho nařkla ze střetu zájmů.")
                    }
                });
                Afery.Add("uklid-praha-cssd", new HlidacStatu.Lib.Analysis.TemplatedQuery()
                {
                    Text = "Úklidové služby pro firmy členů ČSSD",
                    Description = "Zakázky pro firmy Premio Invest a Lasesmed, které vlastní členové ČSSD a roky dostávaly stovky milionů za úklid v Praze od městských organizací, kde mají vliv sociálnědemokratičtí politici.",
                    Query = "ico:26746590 OR ico:28363809",
                    UrlTemplate = "/HledatSmlouvy?Q={0}",
                    Links = new HlidacStatu.Lib.Analysis.TemplatedQuery.AHref[]{
                        new HlidacStatu.Lib.Analysis.TemplatedQuery.AHref("https://zpravy.aktualne.cz/domaci/uklid-prahy-jako-stranicky-byznys-clenove-cssd-vlastni-firmy/r~328647c27c2811e7954a002590604f2e/","Úklid Prahy jako byznys ČSSD. Její členové mají firmy, které žijí ze stamilionových zakázek od města"),
                    }
                });
                Afery.Add("eet", new HlidacStatu.Lib.Analysis.TemplatedQuery()
                {
                    Text = "EET",
                    Description = "Smlouvy pokrývající vývoj a provoz EET. ",
                    Query = "(ico:03630919 OR ico:72054506 OR ico:72080043) AND (EET OR ADIS)",
                    UrlTemplate = "/HledatSmlouvy?Q={0}",
                    Links = new HlidacStatu.Lib.Analysis.TemplatedQuery.AHref[]{
                        new HlidacStatu.Lib.Analysis.TemplatedQuery.AHref("https://dotyk.denik.cz/publicistika/eet-babisuv-nepruhledny-system-na-vymahani-dani-20160915.html","EET: Babišův neprůhledný systém na vymáhání daní"),
                        new HlidacStatu.Lib.Analysis.TemplatedQuery.AHref("https://www.hlidacstatu.cz/texty/10x-predrazene-eet-skutecne-naklady-na-eet/","10x předražené EET: skutečné náklady na EET"),
                    }
                });
                Afery.Add("elektronicke-myto", new HlidacStatu.Lib.Analysis.TemplatedQuery()
                {
                    Text = "Elektronické mýto",
                    Description = @"Smlouvy související elektronickým mýtem.",
                    Query = "\"elektronické mýto\"",
                    UrlTemplate = "/HledatSmlouvy?Q={0}"
                });
                Afery.Add("rsd-s-omezenou-soutezi", new HlidacStatu.Lib.Analysis.TemplatedQuery()
                {
                    Text = "Smlouvy ŘSD s omezenou soutěží",
                    Description = @"Smlouvy ŘSD uzavřené v užším řízení či v jednacím řízení bez uveřejnění.",
                    Query = "icoPlatce:65993390 AND ( \"stavební práce v užším řízení\" OR \"jednací řízení bez uveřejnění\") ",
                    UrlTemplate = "/HledatSmlouvy?Q={0}"
                });


                initialized = true;
            } //lock
            HlidacStatu.Util.Consts.Logger.Info("Static data - Init DONE");

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                HlidacStatu.Util.Consts.Logger.Info("Static data - FirmyNazvyOnlyAscii starting thread");

                foreach (var kv in FirmyNazvy.Get())
                {
                    var jmenoa = Devmasters.Core.TextUtil.RemoveDiacritics(kv.Value.Jmeno).Trim().ToLower();
                    var ico = kv.Key;
                    if (!FirmyNazvyOnlyAscii.ContainsKey(jmenoa))
                        FirmyNazvyOnlyAscii[jmenoa] = new string[] { ico };
                    else if (!FirmyNazvyOnlyAscii[jmenoa].Contains(ico))
                    {
                        var v = FirmyNazvyOnlyAscii[jmenoa];
                        FirmyNazvyOnlyAscii[jmenoa] = v.Union(new string[] { ico }).ToArray();
                    }
                }
                HlidacStatu.Util.Consts.Logger.Info("Static data - FirmyNazvyOnlyAscii thread finished");
            }).Start();

        }


    }
}