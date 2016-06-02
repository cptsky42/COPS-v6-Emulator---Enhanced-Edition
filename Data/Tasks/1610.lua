--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:13 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask1610(client, idx)
    name = "OldMiner"
    face = 1

    if (idx == 0) then

        text(client, "It was said that some rare BlueMice appear in the forest mine cave, and carry treasures with them. I once caught")
        text(client, "one, but got nothing from it. Do you want to try your luck?")
        link(client, "Yeah, why not?", 1)
        link(client, "I don`t believe it.", 255)
        pic(client, 67)
        create(client)

    elseif (idx == 1) then

        text(client, "You must use Silver or Gold Needle to catch this kind of mouse. I do have the stuff. If you want, I can exchange")
        text(client, "Silver Needle for a DragonGem, PhoenixGem KylinGem, or RainbowGem; and exchange GoldNeedle for FuryGem, MoonGem or VioletGem.")
        link(client, "What is the difference?", 2)
        link(client, "I want Gold Needle.", 3)
        link(client, "I want Silver Needle.", 4)
        pic(client, 67)
        create(client)

    elseif (idx == 2) then

        text(client, "The chance of getting treasures is a little bit higher if you use GoldNeedle. But it`s invariable that you must be patient.")
        link(client, "So I want GoldNeedle.", 3)
        link(client, "Even so, I want SilverNeedle.", 4)
        link(client, "Oh, I see.", 255)
        pic(client, 67)
        create(client)

    elseif (idx == 3) then

        if hasItem(client, 722511, 1) then

            text(client, "You have got the needle. Hurry to the forest mine cave to catch BlueMouse, it is rare.")
            link(client, "Ok, I will hurry up.", 255)
            pic(client, 67)
            create(client)

        else

            if hasItem(client, 722510, 1) then

                text(client, "You have got the needle. Hurry to the forest mine cave to catch BlueMouse, it is rare.")
                link(client, "Ok, I will hurry up.", 255)
                pic(client, 67)
                create(client)

            else

                if hasItem(client, 700061, 1) then

                    text(client, "You do have the gem I want. Are you sure to exchange Gold Needle?")
                    link(client, "Yes. I want GoldNeedle.", 5)
                    link(client, "No. I change my mind.", 255)
                    pic(client, 67)
                    create(client)

                else

                    if hasItem(client, 700021, 1) then

                        text(client, "You do have the gem I want. Are you sure to exchange Gold Needle?")
                        link(client, "Yes. I want GoldNeedle.", 6)
                        link(client, "No. I change my mind.", 255)
                        pic(client, 67)
                        create(client)

                    else

                        if hasItem(client, 700051, 1) then

                            text(client, "You do have the gem I want. Are you sure to exchange Gold Needle?")
                            link(client, "Yes. I want GoldNeedle.", 7)
                            link(client, "No. I change my mind.", 255)
                            pic(client, 67)
                            create(client)

                        else

                            text(client, "I am sorry that you should use normal FuryGem, MoonGem or VioletGem in exchange for a GoldNeedle.")
                            link(client, "Ok, I will get it ready soon.", 255)
                            link(client, "Change it to SilverNeedle.", 4)
                            pic(client, 67)
                            create(client)

                        end

                    end

                end

            end

        end

    elseif (idx == 4) then

        if hasItem(client, 722511, 1) then

            text(client, "You have got the needle. Hurry to the forest mine cave to catch BlueMouse, it is rare.")
            link(client, "Ok, I will hurry up.", 255)
            pic(client, 67)
            create(client)

        else

            if hasItem(client, 722510, 1) then

                text(client, "You have got the needle. Hurry to the forest mine cave to catch BlueMouse, it is rare.")
                link(client, "Ok, I will hurry up.", 255)
                pic(client, 67)
                create(client)

            else

                if hasItem(client, 700031, 1) then

                    text(client, "You do have the gem I want. Are you sure to exchange Silver Needle?")
                    link(client, "Yes. I want SilverNeedle.", 8)
                    link(client, "No. I change my mind.", 255)
                    pic(client, 67)
                    create(client)

                else

                    if hasItem(client, 700041, 1) then

                        text(client, "You do have the gem I want. Are you sure to exchange Silver Needle?")
                        link(client, "Yes. I want SilverNeedle.", 9)
                        link(client, "No. I change my mind.", 255)
                        pic(client, 67)
                        create(client)

                    else

                        if hasItem(client, 700001, 1) then

                            text(client, "You do have the gem I want. Are you sure to exchange Silver Needle?")
                            link(client, "Yes. I want SilverNeedle.", 10)
                            link(client, "No. I change my mind.", 255)
                            pic(client, 67)
                            create(client)

                        else

                            if hasItem(client, 700011, 1) then

                                text(client, "You do have the gem I want. Are you sure to exchange Silver Needle?")
                                link(client, "Yes. I want SilverNeedle.", 11)
                                link(client, "No. I change my mind.", 255)
                                pic(client, 67)
                                create(client)

                            else

                                text(client, "I am sorry that you should use normal DragonGem, PhoenixGem KylinGem, or RainbowGem in exchange for a SilverNeedle.")
                                link(client, "Ok, I will get it ready soon.", 255)
                                link(client, "Then change it to GoldNeedle", 3)
                                pic(client, 67)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 5) then

        spendItem(client, 700061, 1)
        awardItem(client, "722511", 1)
        sendSysMsg(client, "You got a GoldNeedle.", 2005)

    elseif (idx == 6) then

        spendItem(client, 700021, 1)
        awardItem(client, "722511", 1)
        sendSysMsg(client, "You got a GoldNeedle.", 2005)

    elseif (idx == 7) then

        spendItem(client, 700051, 1)
        awardItem(client, "722511", 1)
        sendSysMsg(client, "You got a GoldNeedle.", 2005)

    elseif (idx == 8) then

        spendItem(client, 700031, 1)
        awardItem(client, "722510", 1)
        sendSysMsg(client, "You got a SilverNeedle.", 2005)

    elseif (idx == 9) then

        spendItem(client, 700041, 1)
        awardItem(client, "722510", 1)
        sendSysMsg(client, "You got a SilverNeedle.", 2005)

    elseif (idx == 10) then

        spendItem(client, 700001, 1)
        awardItem(client, "722510", 1)
        sendSysMsg(client, "You got a SilverNeedle.", 2005)

    elseif (idx == 11) then

        spendItem(client, 700011, 1)
        awardItem(client, "722510", 1)
        sendSysMsg(client, "You got a SilverNeedle.", 2005)

    end

end
