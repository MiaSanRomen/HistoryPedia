using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryPedia.Models;

namespace HistoryPedia.Data
{
    public static class DataClass
    {
        public static void Initialize(ArticleContext context)
        {
            if (!context.Articles.Any())
            {
                context.Articles.AddRange(
                    new Article
                    {
                        Name = "Roman Empire",
                        ShirtInfo = "Empire of ancient Romans",
                        Info =
                            "The Roman Empire (Latin: Imperium Romanum [ɪmˈpɛri.ũː roːˈmaːnũː]; Koinē Greek: Βασιλεία τῶν Ῥωμαίων, romanized: Basileía tōn Rhōmaíōn) was the post-Republican period of ancient Rome. As a polity it included large territorial holdings around the Mediterranean Sea in Europe, Northern Africa, and Western Asia ruled by emperors. From the accession of Caesar Augustus to the military anarchy of the third century, it was a principate with Italy as metropole of the provinces and the city of Rome as sole capital (27 BC – 286 AD). After the military crisis, the empire was ruled by multiple emperors who shared rule over the Western Roman Empire (based in Milan and later in Ravenna) and over the Eastern Roman Empire (also known as the Byzantine Empire; centred on Nicomedia and Antioch, later based in Constantinople). Rome remained the nominal capital of both parts until 476 AD, when the imperial insignia were sent to Constantinople, following the capture of Ravenna by the barbarians of Odoacer and the subsequent deposition of Romulus Augustulus. The fall of the Western Roman Empire to Germanic kings, along with the hellenization of the Eastern Roman Empire into the Byzantine Empire, conventionally marks the end of Ancient Rome and the beginning of the Middle Ages.",
                        ImageName = "RomanEmpIco",

                        Blocks = new List<BlockInfo>()
                        {
                            new BlockInfo
                            {
                                BlockName  = "History",
                                Text =
                                    "The history of the Roman Empire covers the history of ancient Rome from the fall of the Roman Republic in 27 BC until the abdication of Romulus Augustulus in AD 476 in the West, and the Fall of Constantinople in the East in AD 1453. Ancient Rome became a territorial empire while still a republic, but was then ruled by Roman emperors beginning with Augustus (r. 27 BC – 14 AD), becoming the Roman Empire following the death of the last republican dictator, the first emperor's adoptive father Julius Caesar.",
                                BlockImageName = "RomanEmp1",
                            },
                            new BlockInfo
                            {
                                BlockName = "Emperor",
                                Text =
                                    "The Latin word imperator derives from the stem of the verb imperare, meaning 'to order, to command'. It was originally employed as a title roughly equivalent to commander under the Roman Republic. Later it became a part of the titulature of the Roman Emperors as part of their cognomen. The English word emperor derives from imperator via Old French: Empereür. The Roman emperors themselves generally based their authority on multiple titles and positions, rather than preferring any single title. Nevertheless, imperator was used relatively consistently as an element of a Roman ruler's title throughout the principate and the dominate.",
                                BlockImageName = "RomanEmp2",
                            },

                        }
                    },
                    new Article
                    {
                        Name = "Roman Republic",
                        ShirtInfo = "Republic of ancient Romans",
                        Info =
                            "The Roman Republic (Latin: Rēs pūblica Rōmāna [ˈreːs ˈpuːblɪka roːˈmaːna]) was the era of classical Roman civilization, led by the Roman people, beginning with the overthrow of the Roman Kingdom, traditionally dated to 509 BC, and ending in 27 BC with the establishment of the Roman Empire. During this period, Rome's control expanded from the city's immediate surroundings to hegemony over the entire Mediterranean world.",
                        ImageName = "RepublicIco",

                        Blocks = new List<BlockInfo>()
                        {
                            new BlockInfo
                            {
                                BlockName = "History",
                                Text =
                                    "Unlike the Pax Romana of the Roman Empire, the Republic was in a state of quasi-perpetual war throughout its existence. Its first enemies were its Latin and Etruscan neighbours as well as the Gauls, who even sacked the city in 387 BC. The Republic nonetheless demonstrated extreme resilience and always managed to overcome its losses, however catastrophic. After the Gallic Sack, Rome conquered the whole Italian peninsula in a century, which turned the Republic into a major power in the Mediterranean. The Republic's greatest enemy was doubtless Carthage, against which it waged three wars. The Punic general Hannibal famously invaded Italy by crossing the Alps and inflicted on Rome two devastating defeats at Lake Trasimene and Cannae, but the Republic once again recovered and won the war thanks to Scipio Africanus at the Battle of Zama in 202 BC. With Carthage defeated, Rome became the dominant power of the ancient Mediterranean world. It then embarked on a long series of difficult conquests, after having notably defeated Philip V and Perseus of Macedon, Antiochus III of the Seleucid Empire, the Lusitanian Viriathus, the Numidian Jugurtha, the Pontic king Mithridates VI, the Gaul Vercingetorix, and the Egyptian queen Cleopatra.",
                                BlockImageName = "Republic1",
                            },
                            new BlockInfo
                            {
                                BlockName = "Religion",
                                Text =
                                    "Religion in ancient Rome includes the ancestral ethnic religion of the city of Rome that the Romans used to define themselves as a people, as well as the religious practices of peoples brought under Roman rule, in so far as they became widely followed in Rome and Italy. The Romans thought of themselves as highly religious, and attributed their success as a world power to their collective piety (pietas) in maintaining good relations with the gods. The Romans are known for the great number of deities they honored, a capacity that earned the mockery of early Christian polemicists.",
                                BlockImageName = "Republic2",
                            },

                        }
                    },
                    new Article
                    {
                        Name = "Byzantine Empire",
                        ShirtInfo = "Eastern part of Roman Empire",
                        Info =
                            "The Byzantine Empire, also referred to as the Eastern Roman Empire, or Byzantium, was the continuation of the Roman Empire in its eastern provinces during Late Antiquity and the Middle Ages, when its capital city was Constantinople (modern Istanbul, formerly Byzantium). It survived the fragmentation and fall of the Western Roman Empire in the 5th century AD and continued to exist for an additional thousand years until it fell to the Ottoman Empire in 1453.[1] During most of its existence, the empire was the most powerful economic, cultural, and military force in Europe. Byzantine Empire is a term created after the end of the realm; its citizens continued to refer to their empire simply as the Roman Empire (Greek: Βασιλεία Ῥωμαίων, tr. Basileía Rhōmaíōn; Latin: Imperium Romanum), or Romania (Greek: Ῥωμανία, romanized: Rhōmanía), and to themselves as Romans (Greek: Ῥωμαῖοι, romanized: Rhōmaîoi). Several signal events from the 4th to 6th centuries mark the period of transition during which the Roman Empire's Greek East and Latin West diverged. Constantine I (r. 324–337) reorganised the empire, made Constantinople the new capital and legalised Christianity. Under Theodosius I (r. 379–395), Christianity became the state religion and other religious practices were proscribed. In the reign of Heraclius (r. 610–641), the Empire's military and administration were restructured and adopted Greek for official use in place of Latin.",
                        ImageName = "ByzantIco",

                        Blocks = new List<BlockInfo>()
                        {
                            new BlockInfo
                            {
                                BlockName = "History",
                                Text =
                                    "This history of the Byzantine Empire covers the history of the Eastern Roman Empire from late antiquity until the Fall of Constantinople in 1453 AD. Several events from the 4th to 6th centuries mark the transitional period during which the Roman Empire's east and west divided. In 285, the emperor Diocletian (r. 284–305) partitioned the Roman Empire's administration into eastern and western halves.[1] Between 324 and 330, Constantine I (r. 306–337) transferred the main capital from Rome to Byzantium, later known as Constantinople (City of Constantine) and Nova Roma (New Rome). Under Theodosius I (r. 379–395), Christianity became the Empire's official state religion and others such as Roman polytheism were proscribed. And finally, under the reign of Heraclius (r. 610–641), the Empire's military and administration were restructured and adopted Greek for official use instead of Latin. Thus, although it continued the Roman state and maintained Roman state traditions, modern historians distinguish Byzantium from ancient Rome insofar as it was oriented towards Greek rather than Latin culture, and characterised by Orthodox Christianity rather than Roman polytheism.",
                                BlockImageName = "Byzant1",
                            },
                            new BlockInfo
                            {
                                BlockName = "Economy",
                                Text =
                                    "The Byzantine economy was among the most robust economies in the Mediterranean for many centuries. Constantinople was a prime hub in a trading network that at various times extended across nearly all of Eurasia and North Africa. Some scholars argue that, up until the arrival of the Arabs in the 7th century, the Eastern Roman Empire had the most powerful economy in the world. The Arab conquests, however, would represent a substantial reversal of fortunes contributing to a period of decline and stagnation. Constantine V's reforms (c. 765) marked the beginning of a revival that continued until 1204. From the 10th century until the end of the 12th, the Byzantine Empire projected an image of luxury, and the travelers were impressed by the wealth accumulated in the capital. All this changed with the arrival of the Fourth Crusade, which was an economic catastrophe. The Palaiologoi tried to revive the economy, but the late Byzantine state would not gain full control of either the foreign or domestic economic forces.",
                                BlockImageName = "Byzant2",
                            },

                        }
                    },
                    new Article
                    {
                        Name = "Ancient Carthage",
                        ShirtInfo = "Ancient Phoenician city-state",
                        Info =
                            "Carthage (/ˈkɑːrθədʒ/; Punic: 𐤒𐤓𐤕𐤟𐤇𐤃𐤔𐤕, romanized: Qart-ḥadašt, lit. 'New City'; Latin: Carthāgō) was an ancient Phoenician city-state and civilization located in present-day Tunisia. Founded around 814 BC as a colony of Tyre, within centuries it became the center of the Carthaginian Empire, a major commercial and maritime power that dominated the western Mediterranean until the mid third century BC.",
                        ImageName = "CarthageIco",

                        Blocks = new List<BlockInfo>()
                        {
                            new BlockInfo
                            {
                                BlockName = "History",
                                Text =
                                    "The city of Carthage was founded in the 9th century BC on the coast of Northwest Africa, in what is now Tunisia, as one of a number of Phoenician settlements in the western Mediterranean created to facilitate trade from the city of Tyre on the coast of what is now Lebanon. The name of both the city and the wider republic that grew out of it, Carthage developed into a significant trading empire throughout the Mediterranean. The date from which Carthage can be counted as an independent power cannot exactly be determined, and probably nothing distinguished Carthage from the other Phoenician colonies in Northwest Africa and the Mediterranean during 800–700 BC. By the end of the 7th century BC, Carthage was becoming one of the leading commercial centres of the West Mediterranean region. After a long conflict with the emerging Roman Republic, known as the Punic Wars (264–146 BC), Rome finally destroyed Carthage in 146 BC. A Roman Carthage was established on the ruins of the first. Roman Carthage was eventually destroyed—its walls torn down, its water supply cut off, and its harbours made unusable—following its conquest by Arab invaders at the close of the 7th century. It was replaced by Tunis as the major regional centre, which has spread to include the ancient site of Carthage in a modern suburb.",
                                BlockImageName = "Carthage1",
                            },
                            new BlockInfo
                            {
                                BlockName = "Government and politics",
                                Text =
                                    "Like most ancient societies, including the Phoenician city-states, Carthage was initially ruled as a monarchy, although modern scholars debate whether this stemmed from a misunderstanding by Greek writers. Traditionally, most Phoenician kings did not exercise absolute power, but consulted with a body of advisors called the Adirim (mighty ones), which was likely comprised of the wealthiest members of society, namely merchants.[121] Carthage seems to have been ruled by a similar body known as the Blm, made up of nobles responsible for all important matters of state, including religion, administration, and the military.[120] Within this cabal was a hierarchy topped by the dominant family, usually the wealthiest members of the merchant class, which had some sort of executive power. Records indicate that different families held power at different times, suggesting a nonhereditary system of government dependent on the support or approval of the consultative body.",
                                BlockImageName = "Carthage2",
                            },

                        }
                    },
                    new Article
                    {
                        Name = "Punic wars",
                        ShirtInfo = "Series of wars between Rome and Carthage",
                        Info =
                            "The Punic Wars were a series of three wars between 264 and 146 BC fought by the states of Rome and Carthage. The First Punic War broke out in Sicily in 264 BC as a result of Rome's expansionary attitude combined with Carthage's proprietary approach to the island. At the start of the war Carthage was the dominant power of the western Mediterranean, with an extensive maritime empire; while Rome was a rapidly expanding power in Italy, with a strong army but a weak navy. The fighting took place primarily on the Mediterranean island of Sicily and its surrounding waters, and also in North Africa, Corsica and Sardinia. It lasted 23 years, until 241 BC, when after immense materiel and human losses on both sides the Carthaginians were defeated. By the terms of the peace treaty Carthage paid large reparations and Sicily was annexed as a Roman province. The end of the war sparked a major but unsuccessful revolt within the Carthaginian Empire known as the Mercenary War.",
                        ImageName = "PunicIco",

                        Blocks = new List<BlockInfo>()
                        {
                            new BlockInfo
                            {
                                BlockName = "First Punic War, 264–241 BC",
                                Text =
                                    "The First Punic War (264–241 BC) was the first of three wars fought between Carthage and Rome, the two main powers of the western Mediterranean in the early 3rd century BC. For 23 years, in the longest continuous conflict and greatest naval war of antiquity, the two powers struggled for supremacy. The wars were fought primarily on the Mediterranean island of Sicily and its surrounding waters, and also in North Africa. After immense material and human losses on both sides the Carthaginians were defeated.",
                                BlockImageName = "Punic1",
                            },
                            new BlockInfo
                            {
                                BlockName = "Second Punic War, 218–201 BC",
                                Text =
                                    "The Second Punic War (218–201 BC) was the second of three wars fought between Carthage and Rome, the two main powers of the western Mediterranean in the 3rd century BC. For 17 years the two states struggled for supremacy, primarily in Italy and Iberia, but also on the islands of Sicily and Sardinia and, towards the end of the war, in North Africa. After immense material and human losses on both sides the Carthaginians were defeated. Macedonia, Syracuse and several Numidian kingdoms were drawn into the fighting; and Iberian and Gallic forces fought on both sides. There were three main military theatres during the war: Italy, where the Carthaginian general Hannibal defeated the Roman legions repeatedly, with occasional subsidiary campaigns in Sicily, Sardinia and Greece; Iberia, where Hasdrubal, a younger brother of Hannibal, defended the Carthaginian colonial cities with mixed success until moving into Italy; and Africa, where the war was decided.",
                                BlockImageName = "Punic2",
                            },
                            new BlockInfo
                            {
                                BlockName = "Third Punic War, 149–146 BC",
                                Text =
                                    "The Third Punic War was the third and last of the Punic Wars fought between Carthage and Rome, and lasted from 149 to 146 BC. The war was fought entirely within Carthaginian territory, in modern northern Tunisia. When the Second Punic War ended in 201 BC, one of the terms of the peace treaty prohibited Carthage from waging war without Rome's permission. Rome's ally, King Masinissa of Numidia, exploited this to repeatedly raid and seize Carthaginian territory with impunity. In 149 BC Carthage sent an army, under Hasdrubal, against Masinissa, the treaty notwithstanding. The campaign ended in disaster as the Battle of Oroscopa ended with a Carthaginian defeat and the surrender of the Carthaginian army. Anti-Carthaginian factions in Rome used the illicit military action as a pretext to prepare a punitive expedition.",
                                BlockImageName = "Punic3",
                            },

                        }
                    },
                    new Article
                    {
                        Name = "Roman military",
                        ShirtInfo = "Army of ancient Romans",
                        Info =
                            "The structural history of the Roman military concerns the major transformations in the organization and constitution of ancient Rome's armed forces, 'the most effective and long - lived military institution known to history.' From its origins around 800 BC to its final dissolution in AD 476 with the demise of the Western Roman Empire, Rome's military organization underwent substantial structural change. At the highest level of structure, the forces were split into the Roman army and the Roman navy, although these two branches were less distinct than in many modern national defense forces. Within the top levels of both army and navy, structural changes occurred as a result of both positive military reform and organic structural evolution. These changes can be divided into four distinct phases.",
                        ImageName = "ArmyIco",

                        Blocks = new List<BlockInfo>()
                        {
                            new BlockInfo
                            {
                                BlockName = "Phase I",
                                Text =
                                    "The army was derived from obligatory annual military service levied on the citizenry, as part of their duty to the state. During this period, the Roman army would wage seasonal campaigns against largely local adversaries.",
                                BlockImageName = "Army1",
                            },
                            new BlockInfo
                            {
                                BlockName = "Phase II",
                                Text =
                                    "As the extent of the territories falling under Roman control expanded and the size of the forces increased, the soldiery gradually became salaried professionals. As a consequence, military service at the lower (non-salaried) levels became progressively longer-term. Roman military units of the period were largely homogeneous and highly regulated. The army consisted of units of citizen infantry known as legions (Latin: legiones) as well as non-legionary allied troops known as auxilia. The latter were most commonly called upon to provide light infantry, logistical, or cavalry support.",
                                BlockImageName = "Army2",
                            },
                            new BlockInfo
                            {
                                BlockName = "Phase III",
                                Text =
                                    "At the height of the Roman Empire's power, forces were tasked with manning and securing the borders of the vast provinces which had been brought under Roman control. Serious strategic threats were less common in this period and emphasis was placed on preserving gained territory. The army underwent changes in response to these new needs and became more dependent on fixed garrisons than on march-camps and continuous field operations.",
                                BlockImageName = "Army3",
                            },
                            new BlockInfo
                            {
                                BlockName = "Phase IV",
                                Text =
                                    "As Rome began to struggle to keep control over its sprawling territories, military service continued to be salaried and professional for Rome's regular troops. However, the trend of employing allied or mercenary elements was expanded to such an extent that these troops came to represent a substantial proportion of the armed forces. At the same time, the uniformity of structure found in Rome's earlier military disappeared. Soldiery of the era ranged from lightly armed mounted archers to heavy infantry, in regiments of varying size and quality. This was accompanied by a trend in the late empire of an increasing predominance of cavalry rather than infantry troops, as well as a requirement for more mobile operations. In this period there was more focus (on all frontiers but the east) on smaller units of independently-operating troops, engaging less in set-piece battles and more in low-intensity, guerilla actions.",
                                BlockImageName = "Army4",
                            },
                        }
                    }
                );
                context.SaveChanges();
            }

            if (!context.Pictures.Any())
            {
                context.Pictures.AddRange(
                    new Picture()
                    {
                        Name = "RomanEmpIco",
                        Path = "/Images/RomanEmpIco.png",
                    },
                    new Picture()
                    {
                        Name = "RomanEmp1",
                        Path = "/Images/RomanEmp1.png",
                        Text = "Map of the Roman Empire"
                    },
                    new Picture()
                    {
                        Name = "RomanEmp2",
                        Path = "/Images/RomanEmp2.jpg",
                        Text = "The first Roman emperor Octavian Augustus"
                    },
                    new Picture()
                    {
                        Name = "ByzantIco",
                        Path = "/Images/ByzantIco.png",
                    },
                    new Picture()
                    {
                        Name = "Byzant1",
                        Path = "/Images/Byzant1.jpg",
                        Text = "Map of the Byzantine Empire"
                    },
                    new Picture()
                    {
                        Name = "Byzant2",
                        Path = "/Images/Byzant2.jpg",
                        Text = "Byzantine money"
                    },
                    new Picture()
                    {
                        Name = "RepublicIco",
                        Path = "/Images/RepublicIco.png",
                    },
                    new Picture()
                    {
                        Name = "Republic1",
                        Path = "/Images/Republic1.jpg",
                        Text = "Senate of the republic"
                    },
                    new Picture()
                    {
                        Name = "Republic2",
                        Path = "/Images/Republic2.jpg",
                        Text = "Survived temple in Rome"
                    },
                    new Picture()
                    {
                        Name = "CarthageIco",
                        Path = "/Images/CarthageIco.png",
                    },
                    new Picture()
                    {
                        Name = "Carthage1",
                        Path = "/Images/Carthage1.jpg",
                        Text = "View on ancient carthage"
                    },
                    new Picture()
                    {
                        Name = "Carthage2",
                        Path = "/Images/Carthage2.jpg",
                        Text = "Carthage's most popular leader Hannibal Barka"
                    },
                    new Picture()
                    {
                        Name = "PunicIco",
                        Path = "/Images/PunicIco.jpg",
                    },
                    new Picture()
                    {
                        Name = "Punic1",
                        Path = "/Images/Punic1.jpg",
                        Text = "Sea battle of the first punic war"
                    },
                    new Picture()
                    {
                        Name = "Punic2",
                        Path = "/Images/Punic2.jpg",
                        Text = "Battle of the Cannaes"
                    },
                    new Picture()
                    {
                        Name = "Punic3",
                        Path = "/Images/Punic3.jpg",
                        Text = "Assault of Carthage"
                    },
                    new Picture()
                    {
                        Name = "ArmyIco",
                        Path = "/Images/ArmyIco.jpg",
                    },
                    new Picture()
                    {
                        Name = "Army1",
                        Path = "/Images/Army1.jpg",
                        Text = "Early roman units"
                    },
                    new Picture()
                    {
                        Name = "Army2",
                        Path = "/Images/Army2.jpg",
                        Text = "Caesar's legion"
                    },
                    new Picture()
                    {
                        Name = "Army3",
                        Path = "/Images/Army3.jpg",
                        Text = "Legionaries of Roman Empire"
                    },
                    new Picture()
                    {
                        Name = "Army4",
                        Path = "/Images/Army4.jpg",
                        Text = "Late roman soldiers"
                    },
                    new Picture()
                    {
                        Name = "DefIco",
                        Path = "/Images/DefIco.png",
                    },
                    new Picture()
                    {
                        Name = "Def1",
                        Path = "/Images/Def1.jpg",
                        Text = "What a picture!"
                    }
                );
                context.SaveChanges();
            }
        }

    }
}
