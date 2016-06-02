--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:11 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask425(client, idx)
    name = "OldGeneralYang"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getProfession(client) == 10 then

                if checkUserTask(client, 1) then

                    text(client, "KnowItAll on the bridge will teleport you to Twin City to start playing. I`ve a strong feeling that you`ll be a hero.")
                    link(client, "I have questions still.", 1)
                    link(client, "Let`s say goodbye.", 255)
                    pic(client, 91)
                    create(client)

                else

                    text(client, "My old fighting days were called to mind at sight of you. Let me tell you some knacks to protect you for the future.")
                    link(client, "Thank you for your teaching.", 2)
                    link(client, "Stroll in the other place.", 255)
                    pic(client, 91)
                    create(client)

                end

            else

                if getProfession(client) == 20 then

                    if checkUserTask(client, 1) then

                        text(client, "KnowItAll on the bridge will teleport you to Twin City to start playing. I`ve a strong feeling that you`ll be a hero.")
                        link(client, "I have questions still.", 1)
                        link(client, "Let`s say goodbye.", 255)
                        pic(client, 91)
                        create(client)

                    else

                        text(client, "My old fighting days were called to mind at sight of you. Let me tell you some knacks to protect you for the future.")
                        link(client, "Thank you for your teaching.", 2)
                        link(client, "Stroll in the other place.", 255)
                        pic(client, 91)
                        create(client)

                    end

                else

                    text(client, "I am here to teach weapon skill to Warrior and Trojan. Sorry that I am unable to help you.")
                    link(client, "Thanks anyway.", 255)
                    pic(client, 91)
                    create(client)

                end

            end

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Well, what questions?")
            link(client, "Where to get weapons from?", 3)
            link(client, "Where to learn skills from?", 4)
            link(client, "No problem.", 255)
            pic(client, 91)
            create(client)

        end

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "I`ll teach you a weapon skill. Are you ready?")
            link(client, "I`m ready.", 5)
            pic(client, 91)
            create(client)

        end

    elseif (idx == 3) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Pedlar in Twin City sells all sorts of elementary weapons. For better weapons you need buy from Blacksmith.")
            text(client, "If you are lucky enough you may get weapons after the monsters you`ve killed.")
            link(client, "Thank you.", 255)
            pic(client, 91)
            create(client)

        end

    elseif (idx == 4) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "NPCs in Job Center of Twin City will teach you skills and spells. Monsters may drop skill books too. Pharmacist in")
            text(client, "the Market also sell them at considerable price.")
            link(client, "Thank you.", 255)
            pic(client, 91)
            create(client)

        end

    elseif (idx == 5) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            action = randomAction(client, 1, 8)
            if action == 1 then

                play(client, "sound/train.wav", false)

                sendSysMsg(client, "You`ve learned WideStrike!", 2005)

                text(client, "You are so talented that you`ve learned WideStrike. I will send you a WoodGlaive. You can arm it after some")
                text(client, "trainings. WideStrike will be activated automatically and become more powerful as you use glaives.")
                awardMagic(client, 1250, 0)
                awardItem(client, "510005 0 0 0 0 0 0 0 0 0 0", 1)
                setUserTask(client, 1)
                link(client, "Thank you very much!", 6)
                pic(client, 91)
                create(client)


            elseif action == 2 then

                play(client, "sound/train.wav", false)

                sendSysMsg(client, "You`ve learned Boreas!", 2005)

                text(client, "You are so talented that you have learned Boreas quickly. I present you a WoodPoleAxe that you can arm after some")
                text(client, "trainings. Boreas will be activated automatically as you use axes. If you make more exercises, it`ll be more and more powerful!")
                awardMagic(client, 5050, 0)
                awardItem(client, "530005 0 0 0 0 0 0 0 0 0 0", 1)
                setUserTask(client, 1)
                link(client, "Thank you very much!", 6)
                pic(client, 91)
                create(client)


            elseif action == 3 then

                play(client, "sound/train.wav", false)

                sendSysMsg(client, "You`ve learned Halt!", 2005)

                text(client, "You are so talented that you`ve learned Halt. I send you a RatanLonghammer that you can arm after some trainings.")
                text(client, "Halt will be activated automatically as you use longhammer. If you make more exercises, it`ll become more powerful.")
                awardMagic(client, 1300, 0)
                awardItem(client, "540005 0 0 0 0 0 0 0 0 0 0", 1)
                setUserTask(client, 1)
                link(client, "Thank you very much!", 6)
                pic(client, 91)
                create(client)


            elseif action == 4 then

                play(client, "sound/train.wav", false)

                sendSysMsg(client, "You`ve learned StrandedMonster!", 2005)

                text(client, "You`re so genius that you`ve learned StrandedMonster quickly. I send you a WoodHammer that you can arm after")
                text(client, "after some trainings. StrandedMonster will launch automatically as you use hammers. If you exercise more, it`ll be stronger!")
                awardMagic(client, 5020, 0)
                awardItem(client, "580005 0 0 0 0 0 0 0 0 0 0", 1)
                setUserTask(client, 1)
                link(client, "Thank you very much!", 6)
                pic(client, 91)
                create(client)


            elseif action == 5 then

                play(client, "sound/train.wav", false)

                sendSysMsg(client, "You`ve learned WideStrike!", 2005)

                text(client, "You are so talented that you`ve learned WideStrike. I will send you a WoodGlaive. You can arm it after some")
                text(client, "trainings. WideStrike will be activated automatically and become more powerful as you use glaives.")
                awardMagic(client, 1250, 0)
                awardItem(client, "510005 0 0 0 0 0 0 0 0 0 0", 1)
                setUserTask(client, 1)
                link(client, "Thank you very much!", 6)
                pic(client, 91)
                create(client)


            elseif action == 6 then

                play(client, "sound/train.wav", false)

                sendSysMsg(client, "You`ve learned Boreas!", 2005)

                text(client, "You are so talented that you have learned Boreas quickly. I present you a WoodPoleAxe that you can arm after some")
                text(client, "trainings. Boreas will be activated automatically as you use axes. If you make more exercises, it`ll be more and more powerful!")
                awardMagic(client, 5050, 0)
                awardItem(client, "530005 0 0 0 0 0 0 0 0 0 0", 1)
                setUserTask(client, 1)
                link(client, "Thank you very much!", 6)
                pic(client, 91)
                create(client)


            elseif action == 7 then

                play(client, "sound/train.wav", false)

                sendSysMsg(client, "You`ve learned Halt!", 2005)

                text(client, "You are so talented that you`ve learned Halt. I send you a RatanLonghammer that you can arm after some trainings.")
                text(client, "Halt will be activated automatically as you use longhammer. If you make more exercises, it`ll become more powerful.")
                awardMagic(client, 1300, 0)
                awardItem(client, "540005 0 0 0 0 0 0 0 0 0 0", 1)
                setUserTask(client, 1)
                link(client, "Thank you very much!", 6)
                pic(client, 91)
                create(client)


            elseif action == 8 then

                play(client, "sound/train.wav", false)

                sendSysMsg(client, "You`ve learned StrandedMonster!", 2005)

                text(client, "You`re so genius that you`ve learned StrandedMonster quickly. I send you a WoodHammer that you can arm after")
                text(client, "after some trainings. StrandedMonster will launch automatically as you use hammers. If you exercise more, it`ll be stronger!")
                awardMagic(client, 5020, 0)
                awardItem(client, "580005 0 0 0 0 0 0 0 0 0 0", 1)
                setUserTask(client, 1)
                link(client, "Thank you very much!", 6)
                pic(client, 91)
                create(client)


            end


        end

    elseif (idx == 6) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "KnowItAll on the bridge will teleport you to Twin City to start playing. I`ve a strong feeling that you`ll be a hero.")
            link(client, "I have questions still.", 1)
            link(client, "Let`s say goodbye.", 255)
            pic(client, 91)
            create(client)

        end

    end

end
