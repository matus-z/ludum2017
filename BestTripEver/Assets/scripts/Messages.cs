using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ----------------------------------------------------------------
public class Messages
{
    List<String> Msgs;

    public Messages()
    {
        Msgs = new List<String>();

        Msgs.Add("I was fed up with how poor I was at the time.I looked, and I found work where I was able to make lot of money.I heard people talking, even responded sometimes - mostly when there was something to gain.But I stopped listening. I just wanted to OWN stuff. Big apartment, expensive car, brand clothes. I had to have them all.\"");
        Msgs.Add("\"Come on! It\'s not that hard, just slide towards me.\" I called out. \"That\'s easier said than doOoOO...\" she screamed as her legs were flung in the air and her bottom hit the ice rink.");
        Msgs.Add("I didn\'t know she was such a klutz, it seemed really cute to me back then. I helped her stand up as she whined that \"her butt hurts\".");
        Msgs.Add("Red lights.I stopped the car. A thought hit me - I haven’t eaten dinner that day yet. I thought about cooking something up, but I remembered they had this new double-decker burger at the food stall I sometimes go to.And just like that I started eating fast-food all the time. While at it, why not a bottomless cup.The menu was so cheap I had to get two instead...");
        Msgs.Add("The weather was great.Today was my day. \"You better believe me, I can make it a hole-in-one!\" I announced as I took the swing. With red ears I watched the ball slowly descend into a small pond ways off the golf course. Seeing my colleague\'s stupid grin didn\'t help, even less so his remark: \"Must be the wind...\"");
        Msgs.Add("We just stood there, grinning ear to ear while cleaning our teeth.Brush, brush, brush.Being the klutz she always was - a huge piece of drool escaped her mouth and dropped right at my slipper. Pretending I was angry, I stared at her while faking murder intent.We were so happy and careless back then...");
        Msgs.Add("\"Have you seen the latest episode, with the blue alien guy?\" asked my colleague. \"No, not yet - was it good?\" - I politely asked back. \"Don\'t even bother, it was worse than this coffee\" she said while looking into the cup with disgust. \"Yeah, the whole last season sucks - I\'ll probably drop it.\" I sighed as the waitress brought the cake.");
        Msgs.Add("\"Let me grab that for you.\" I said taking the popcorn and cola from my pregnant wife\'s hands.\nAs we walked down the cinema isle I asked her: \"Are you sure you want to see Predator right now? You\'re not very good with jump scares, will you be okay?\" She just looked my way and mumbled that it\'s gonna be fine for sure.");
        Msgs.Add("Initially, I was overcome with joy when our twins were born. But soon I became annoyed and exhausted by the lack of sleep and the children’s constant need for care.My wife and I stopped going out for dinner.At that point I was happy to be left alone with telly and beer all evening. As the kids grew, my habits remained.");
        Msgs.Add("“Daddy, daddy, what is this called?” my little princess asked. “That’s called a lily flower, honey.” I softly responded while putting the flower into her hair - “Here, now you look like a hawaiian dancer.” She asked me - “Daddy, what’s a howaien dancer?” I started explaining - “Do you remember the show...”");
        Msgs.Add("“Moom ?” whined our young son. “Can I have Huggy?” My significant other handed our son the panda plushy and he finally calmed down. “Tsch” clicked our daughter her tongue, “...can’t fall asleep without his toy, what a ninny!” I just sighed and wished to be somewhere else so I wouldn’t have to deal with this... ");
        Msgs.Add("“Hmm, what should I get?” I mumbled as I was trying to select a lemonade.My son told me: “I recommend ice tea for the most refreshing sensation.” I took a small sip, only to cry out in disgust - “What the fuck did you put in this?! You trying to poison me?!” I yelled at my kids while rushing back home to wash my mouth out with with beer.");
        Msgs.Add("“You can’t give them everything they want!” I raised my voice. “I just gave him a single freakin cookie, what’s your problem?!” my wife defended herself. “Why do you always have to fight me?! They’re my children too. You don’t know shit!” I slapped my wife across the face.A red flower blossomed on my wife’s face as she looked at me with resentment. That was the day she took the kids and left our home.");
        Msgs.Add("“Alice’s adventures in wonderland.” I read the title of an old book I used to read to my children a long time ago.They loved how Alice never gave up despite being in a strange, foreign land. I thought to myself: “I wish I had the kind of courage Alice has back then…” as I fell asleep in my empty, grimy house. A half-finished bottle of booze fell out of my hand.");
        Msgs.Add("“I like him a lot, dad, please don’t screw it up for me.” my daughter begged as her new boyfriend approached us. “A sweet piece of fruit for a sweet lady.” he exclaimed and hung a pair of cherries on her ear.She giggled and blushed a little.I took a long look at the boy and asked him “What was your name again?”");
        Msgs.Add("It was the first time I lost contact with my kids for a longer period.I thought they just didn’t like being around me, so I didn’t bother to call.But my ex-wife knew how much they missed me, so she took it upon herself to set up an event for us to come together. We were supposed to go to a theme park. I forgot, and I don’t think the kids ever forgave me for it.");
        Msgs.Add("\"Trust me, you\'re gonna love it.\" she whispered in my ear while softly caressing my thigh.All the blood left my brain.I stood up and followed the blonde into one of the private rooms.Afterwards, I handed over the money.I washed down the rest of my honor with a glass of old fashioned.I didn’t care if She found out about tonight.");
        Msgs.Add("Once, out of the blue, my son came by. He kept ringing the doorbell until I came and let him in. He seemed really happy to see me, holding a basketball in his arms. “Wanna play ball?” he asked. I nodded. Later he told me “Dad, I have cancer.They’re gonna cut off my leg. I wanted to play just one more game with you.”");
        Msgs.Add("For one christmas we managed to actually get together.My ex brought a bunch of food and wine too. We ate, drank and laughed together at the bright memories from past years.I noticed she looked really tired, but didn’t mention it.In the evening they all went. The next day I found out that she fell asleep behind the wheel.");
        Msgs.Add("Numb and unfeeling, I somehow staggered through life so far.I lost my job due to my addictions. The kids couldn’t rely on me for any kind of support. This is the ukulele my son won the national competition with.I was really glad he found a new meaning in his life.I didn’t tell him though, I don’t think they care for me anymore.");
        Msgs.Add("I think I’ll give them a call.Get some help.The least I could do for my children is teach them about decisions and life. I will try to be a real father to them.They may not accept me right away, but I’ll keep trying and maybe someday we can become a real family again.\n\n\t\tTHE END");
        Msgs.Add("I did all of it.Good and bad.There’s nothing left for me.I’m sure noone will even notice I’m gone. Why should I care about them if they don’t even give me a call? It is time.I don’t want to live in this world anymore…\n\n\t\tTHE END");
    }

    public String GetMessage(int index)
    {
        Debug.Log(Msgs);

        if (index < 0 || index >= Msgs.Count)
            return "";

        return Msgs[index];
    }
}
