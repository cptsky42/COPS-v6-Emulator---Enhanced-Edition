--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:51 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30167(client, idx)
    name = "Roy"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721253, 1) then

            text(client, "Are there other things I can help you?")
            link(client, "No. Bye-bye.", 255)
            pic(client, 189)
            create(client)

        else

            if hasItem(client, 721251, 1) then

                if hasItem(client, 721252, 1) then

                    text(client, "EscortChief asked you to look into the case. Why do you come here?")
                    link(client, "Just passing by.", 255)
                    link(client, "Please do me a favor.", 1)
                    pic(client, 189)
                    create(client)

                else

                    text(client, "People in the martial circle called me Roy, in spite of that it is a male name.")
                    link(client, "You must be a heroine.", 255)
                    pic(client, 189)
                    create(client)

                end

            else

                text(client, "People in the martial circle called me Roy, in spite of that it is a male name.")
                link(client, "You must be a heroine.", 255)
                pic(client, 189)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "My information costs a meteor. Give me a meteor then I will answer your question.")
        link(client, "No problem!", 2)
        link(client, "Forget it.", 255)
        pic(client, 189)
        create(client)

    elseif (idx == 2) then

        if hasItem(client, 1088001, 1) then

            text(client, "Ok, what can I do for you?")
            link(client, "Identify my blade.", 3)
            pic(client, 189)
            create(client)

        else

            text(client, "Give a meteor and then I will answer your question.")
            link(client, "I will prepare it.", 255)
            pic(client, 189)
            create(client)

        end

    elseif (idx == 3) then

        if hasItem(client, 721252, 1) then

            text(client, "It is GreenBlade and belongs to Yougo in NewDesert.")
            link(client, "Tell me more details.", 4)
            pic(client, 189)
            create(client)

        else

            text(client, "You are kidding. You did not bring the blade.")
            link(client, "I forgot it.", 255)
            pic(client, 189)
            create(client)

        end

    elseif (idx == 4) then

        text(client, "Judged from the bloodstain, it happened 2 months ago, when the PeaceJade was robbed. It may be the weapon used by the murderer.")
        link(client, "It seems like that.", 5)
        pic(client, 189)
        create(client)

    elseif (idx == 5) then

        text(client, "Right. I heard that the Blade Ghosts stole the KylinJade from Yougo. Yougo and Blade Ghost are enemies.")
        link(client, "It is complicated.", 255)
        link(client, "What does it concern?", 6)
        pic(client, 189)
        create(client)

    elseif (idx == 6) then

        text(client, "In my judgment, BladeGhost persuaded Yougo to kill the bodyguards and robbed the jade. Thus force Escort Bureau disappear.")
        link(client, "Where is PeaceJade?", 7)
        link(client, "It is terrible.", 255)
        pic(client, 189)
        create(client)

    elseif (idx == 7) then

        text(client, "Yougo has no chance to exchange for KylinJade with PeaceJade. So Yougo must still keep the jade.")
        link(client, "What can I do now?", 8)
        pic(client, 189)
        create(client)

    elseif (idx == 8) then

        text(client, "I do not think it is possible to get back the jade by force for Yougo is a tough guy. You can only exchange it with JadeKylin.")
        link(client, "I will find JadeKylin.", 9)
        link(client, "Wow, it is troublesome.", 255)
        pic(client, 189)
        create(client)

    elseif (idx == 9) then

        spendItem(client, 1088001, 1)
        spendItem(client, 721251, 1)
        spendItem(client, 721252, 1)
        awardItem(client, "721253", 1)
        text(client, "I saw Yougo once. Take my letter to him. Hope he will return the PeaceJade and surrender.")
        link(client, "Ok, thank you.", 255)
        pic(client, 189)
        create(client)

    end

end
