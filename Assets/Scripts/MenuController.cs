using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas ViewerCanvas;
    public Canvas MenuCanvas;
    public CameraController CameraScript;

    public Text NameText;
    public Image ConstImage;
    public Text DescText;
    public ScrollRect ScrollView;

    List<TestItem> list = new List<TestItem>();

    void Start()
    {
        MenuCanvas.enabled = false;

        list.Add(new TestItem { Name = "Ursa Major", Image = "Constellations/ursa_major", Desc = "The constellation of Ursa Major has been seen as a bear, usually a female bear, by many distinct civilizations. This may stem from a common oral tradition stretching back more than 13,000 years. Using statistical and phylogenetic tools, Julien d'Huy reconstructs the following Palaeolithic state of the story: 'There is an animal that is a horned herbivore, especially an elk. One human pursues this ungulate. The hunt locates or get to the sky. The animal is alive when it is transformed into a constellation. It forms the Big Dipper'.\n\nIn Burmese, Pucwan Tārā (ပုဇွန် တာရာ, pronounced 'bazun taya') is the name of a constellation comprising stars from the head and forelegs of Ursa Major; pucwan (ပုဇွန်) is a general term for a crustacean, such as prawn, shrimp, crab, lobster, etc.\n\nIn Roman mythology, Jupiter (the king of the gods) lusts after a young woman named Callisto, a nymph of Diana. Juno, Jupiter's jealous wife, discovers that Callisto has a son named Arcas, and believes it is by Jupiter. Juno then transforms the beautiful Callisto into a bear so she no longer attracts Jupiter. Callisto, while in bear form, later encounters her son Arcas. Arcas almost shoots the bear, but to avert the tragedy, Jupiter turns Arcas into a bear too and puts them both in the sky, forming Ursa Major and Ursa Minor. Callisto is Ursa Major and her son, Arcas, is Ursa Minor. In ancient times the name of the constellation was Helike, ('turning'), because it turns around the Pole. In Book Two of Lucan it is called Parrhasian Helice, since Callisto came from Parrhasia in Arcadia, where the story is set. The Odyssey notes that it is the sole constellation that never sinks below the horizon and 'bathes in the Ocean's waves', so it is used as a celestial reference point for navigation. It is also referred to as the 'Wain'.\n\nOne of the few star groups mentioned in the Bible (Job 9:9; 38:32; — Orion and the Pleiades being others), Ursa Major was also pictured as a bear by the Jewish peoples. ('The Bear' was translated as 'Arcturus' in the Vulgate and it persisted in the KJV.)\n\nIn the Finnish language, the asterism is sometimes called with its old Finnish name, Otava. The meaning of the name has been almost forgotten in Modern Finnish; it means a salmon weir. Ancient Finns believed the bear (Ursus arctos) was lowered to earth in a golden basket off the Ursa Major, and when a bear was killed, its head was positioned on a tree to allow the bear's spirit to return to Ursa Major.\n\nThe Iroquois Native Americans interpreted Alioth, Mizar, and Alkaid as three hunters pursuing the Great Bear. According to one version of their myth, the first hunter (Alioth) is carrying a bow and arrow to strike down the bear. The second hunter (Mizar) carries a large pot — the star Alcor — on his shoulder in which to cook the bear while the third hunter (Alkaid) hauls a pile of firewood to light a fire beneath the pot.\n\nThe Wampanoag people (Algonquian) Native Americans referred to Ursa Major as 'maske', meaning 'bear' according to Thomas Morton in The New England Canaan.\n\nThe Wasco-Wishram Native Americans interpreted the constellation as 5 wolves and 2 bears that were left in the sky by Coyote (mythology).\n\nIn Hinduism, Ursa Major is known as Saptarshi, each of the stars representing one of the Saptarshis or Seven Sages viz. Bhrigu, Atri, Angirasa, Vasishta, Pulastya, Pulalaha and Kratu. The fact that the two front stars of the constellations point to the pole star is explained as the boon given to the boy sage Dhruva by Lord Vishnu.\n\nIn Javanese, as known as 'Bintang Kartika'. This name comes from Sanskrit which refers 'krttikã' the same star cluster. In ancient Javanese this brightest seven stars are known as Lintang Wuluh, literally means 'seven stars'. This star cluster is so popular because its emergence into the sky signals the time marker for planting.\n\nIn South Korea, the constellation is referred to as 'the seven stars of the north'. In the related myth, a widow with seven sons found comfort with a widower, but to get to his house required crossing a stream. The seven sons, sympathetic to their mother, placed stepping stones in the river. Their mother, not knowing who put the stones in place, blessed them and, when they died, they became the constellation.\n\nIn Shinto, the seven largest stars of Ursa Major belong to Amenominakanushi, the oldest and most powerful of all kami.\n\nIn China and Japan, the Big Dipper is called the 'North Dipper' 北斗 (Chinese: běidǒu, Japanese: hokuto), and in ancient times, each one of the seven stars had a specific name, often coming themselves from ancient China:\n\n'Pivot' 樞 (C: shū J: sū) is for Dubhe (Alpha Ursae Majoris)\n'Beautiful jade' 璇 (C: xuán J: sen) is for Merak (Beta Ursae Majoris)\n'Pearl' 璣 (C: jī J: ki) is for Phecda (Gamma Ursae Majoris)\n'Authority' 權 (C: quán J: ken) is for Megrez (Delta Ursae Majoris)\n'Measuring rod of jade' 玉衡 (C: yùhéng J: gyokkō) is for Alioth (Epsilon Ursae Majoris)\n'Opening of the Yang' 開陽 (C: kāiyáng J: kaiyō) is for Mizar (Zeta Ursae Majoris)\nAlkaid (Eta Ursae Majoris) has several nicknames: 'Sword' 劍 (C: jiàn J: ken) (short form from 'End of the sword' 劍先 (C: jiàn xiān J: ken saki)), 'Flickering light' 搖光 (C: yáoguāng J: yōkō), or again 'Star of military defeat' 破軍星 (C: pójūn xīng J: hagun sei), because travel in the direction of this star was regarded as bad luck for an army.\nIn Theosophy, it is believed the Seven Stars of the Pleiades focus the spiritual energy of the Seven Rays from the Galactic Logos to the Seven Stars of the Great Bear, then to Sirius, then to the Sun, then to the god of Earth (Sanat Kumara), and finally through the seven Masters of the Seven Rays to the human race.\n\nThe Lakota people call the constellation Wičhákhiyuhapi, or 'Great Bear'." });
        list.Add(new TestItem { Name = "Ursa Minor", Image = "Constellations/ursa_minor", Desc = "In the Babylonian star catalogues, Ursa Minor was known as the 'Wagon of Heaven' (MULMAR.GÍD.DA.AN.NA, also associated with the goddess Damkina). It is listed in the MUL.APIN catalogue, compiled around 1000 BC among the 'Stars of Enlil'—that is, the northern sky.\n\nAccording to Diogenes Laertius, citing Callimachus, Thales of Miletus 'measured the stars of the Wagon by which the Phoenicians sail'. Diogenes identifies these as the constellation of Ursa Minor, which for its reported use by the Phoenicians for navigation at sea were also named Phoinikē. The tradition of naming the northern constellations 'bears' appears to be genuinely Greek, although Homer refers to just a single 'bear'. The original 'bear' is thus Ursa Major, and Ursa Minor was admitted as second, or 'Phoenician Bear' (Ursa Phoenicia, hence Φοινίκη, Phoenice) only later, according to Strabo (I.1.6, C3) due to a suggestion by Thales, who suggested it as a navigation aid to the Greeks, who had been navigating by Ursa Major. In classical antiquity, the celestial pole was somewhat closer to Beta Ursae Minoris than to Alpha Ursae Minoris, and the entire constellation was taken to indicate the northern direction. Since the medieval period, it has become convenient to use Alpha Ursae Minoris (or 'Polaris') as the north star, even though it was still several degrees away from the celestial pole.[a] Its New Latin name of stella polaris was coined only in the early modern period. The ancient name of the constellation is Cynosura (Greek Κυνοσούρα 'dog's tail'). The origin of this name is unclear (Ursa Minor being a 'dog's tail' would imply that another constellation nearby is 'the dog', but no such constellation is known). Instead, the mythographic tradition of Catasterismi makes Cynosura the name of an Oread nymph described as a nurse of Zeus, honoured by the god with a place in the sky. There are various proposed explanations for the name Cynosura. One suggestion connects it to the myth of Callisto, with her son Arcas replaced by her dog being placed in the sky by Zeus. Others have suggested that an archaic interpretation of Ursa Major was that of a Cow, forming a group with Bootes as herdsman, and Ursa Minor as a dog. George William Cox explained it as a variant of Λυκόσουρα, understood as 'wolf's tail' but by him etymologized as 'trail, or train, of light' (i.e. λύκος 'wolf' vs. λύκ- 'light'). Allen points to the Old Irish name of the constellation, drag-blod 'fire trail', for comparison. Brown (1899) suggested a non-Greek origin of the name (a loan from an Assyrian An‑nas-sur‑ra 'high-rising').\n\nAn alternative myth tells of two bears that saved Zeus from his murderous father Cronus by hiding him on Mount Ida. Later Zeus set them in the sky, but their tails grew long from being swung by the god.\n\nBecause Ursa Minor consists of seven stars, the Latin word for 'north' (i.e., where Polaris points) is septentrio, from septem (seven) and triones (oxen), from seven oxen driving a plough, which the seven stars also resemble. This name has also been attached to the main stars of Ursa Major.\n\nIn Inuit astronomy, the three brightest stars—Polaris, Kochab and Pherkad—were known as Nuutuittut 'never moving', though the term is more frequently used in the singular to refer to Polaris alone. The Pole Star is too high in the sky at far northern latitudes to be of use in navigation.\n\nIn Chinese astronomy, the main stars of Ursa Minor are divided between two asterisms: 勾陳 Gòuchén (Curved Array) (including α UMi, δ UMi, ε UMi, ζ UMi, η UMi, θ UMi, λ UMi) and 北極 Běijí (Northern Pole) (including β UMi and γ UMi)." });
    }

    void Update()
    {

    }

    public void OpenMenu(GameObject constellation)
    {
        var index = int.Parse(constellation.name);
        var item = list[index];

        NameText.text = item.Name;
        DescText.text = item.Desc;
        ConstImage.sprite = Resources.Load<Sprite>(item.Image);

        ScrollView.verticalNormalizedPosition = 1;

        MenuCanvas.enabled = true;
        ViewerCanvas.enabled = false;
        CameraScript.enabled = false;
    }

    public void CloseMenu()
    {
        MenuCanvas.enabled = false;
        CameraScript.enabled = true;
        ViewerCanvas.enabled = true;
    }

    class TestItem
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Desc { get; set; }
    }
}
