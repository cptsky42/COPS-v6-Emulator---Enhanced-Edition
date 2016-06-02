--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/1/2015 7:44:05 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3601(client, idx)
    name = "Arthur"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMetempsychosis(client) == 2 then

                text(client, "Wow! You`ve completed your second rebirth. Glad to see you again.")
                link(client, "Nice to meet you.", 255)
                pic(client, 32)
                create(client)

            else

                if getMetempsychosis(client) == 0 then

                    text(client, "You haven`t completed your first rebirth yet. I only offer my service to the level 120+ reborn people.")
                    link(client, "I see.", 255)
                    pic(client, 32)
                    create(client)

                else

                    if getLevel(client) < 120 then

                        text(client, "You haven`t reached level 120 yet. This place is very dangerous. You`d better leave here now.")
                        link(client, "OK.", 255)
                        pic(client, 32)
                        create(client)

                    else

                        if getUserStats(client, 61, 0) >= 1 then

                            text(client, "I feel I am full of strength. After thousands years` wait, my dreams have come true. Thanks a lot for your great ")
                            text(client, "help. I recommended you to Bryan. Let him guide you through your test journey.")
                            text(client, "")
                            link(client, "Thank you very much.", 255)
                            pic(client, 32)
                            create(client)

                        else

                            if hasItem(client, 722731, 3) then

                                text(client, "I am so glad that you`ve made my dreams come true. I will return to the heaven very soon. I`ve reported your ")
                                text(client, "merits to Bryan. He will help you with your tests.")
                                text(client, "")
                                link(client, "Thank you very much.", 1)
                                pic(client, 32)
                                create(client)

                            else

                                if hasItem(client, 722731, 1) then

                                    text(client, "I need three Pure Vigors to restore my force. What else can I do for you?")
                                    link(client, "Make an EvilTooth", 2)
                                    link(client, "Make an ImmortalStone", 3)
                                    link(client, "Make an ImpureVigor", 4)
                                    link(client, "Just passing by.", 255)
                                    pic(client, 32)
                                    create(client)

                                else

                                    if hasItem(client, 722730, 1) then

                                        text(client, "I used to live in Heaven. Since I was trapped by devils, I have been here for thousands years and lost my ")
                                        text(client, "supernatural force. My force can be restored by three PureVigors. Take your ImpureVigor to Cleansing Stove, click")
                                        text(client, "that stove to summon a CleansingDevil. Kill that devil to get a PureVigor.")
                                        text(client, "")
                                        link(client, "Make an EvilTooth.", 2)
                                        link(client, "Make an ImmortalStone.", 3)
                                        link(client, "Make an ImpureVigor", 4)
                                        link(client, "I see.", 255)
                                        pic(client, 32)
                                        create(client)

                                    else

                                        if hasItem(client, 722729, 1) then

                                            text(client, "Kill a Banshee to get a VigorFragment. After you have three VigorFragments, I can make an ImpureVigor for you.")
                                            text(client, "")
                                            link(client, "Make an EvilTooth.", 2)
                                            link(client, "Make an ImmortalStone.", 3)
                                            link(client, "Make an ImpureVigor", 4)
                                            link(client, "I see.", 255)
                                            pic(client, 32)
                                            create(client)

                                        else

                                            if hasItem(client, 722728, 1) then

                                                text(client, "Take an ImmortalStone, Moss, DreamGrass and SoulAroma to FireSeal. Click that seal to summon a Banshee. kill that ")
                                                text(client, "Banshee to get a VigorFragment.")
                                                text(client, "")
                                                link(client, "Make an EvilTooth.", 2)
                                                link(client, "Make an ImmortalStone.", 3)
                                                link(client, "Make an ImpureVigor", 4)
                                                link(client, "I see.", 255)
                                                pic(client, 32)
                                                create(client)

                                            else

                                                if hasItem(client, 722726, 1) then

                                                    text(client, "Kill a SwiftDevil to get a FeatherStone. After you have three FeatherStones, I can make an ImmortalStone for you.")
                                                    text(client, "")
                                                    link(client, "Make an EvilTooth.", 2)
                                                    link(client, "Make an ImmortalStone.", 3)
                                                    link(client, "Make an ImpureVigor", 4)
                                                    link(client, "I see.", 255)
                                                    pic(client, 32)
                                                    create(client)

                                                else

                                                    if hasItem(client, 722721, 1) then

                                                        text(client, "Take an EvilTooth, Moss, DreamGrass and SoulAroma to WaterSeal. Click that seal to summon a SwiftDevil. kill ")
                                                        text(client, "that Devil to get a FeatherStone.")
                                                        text(client, "")
                                                        link(client, "Make an EvilTooth.", 2)
                                                        link(client, "Make an ImmortalStone.", 3)
                                                        link(client, "Make an ImpureVigor", 4)
                                                        link(client, "I see.", 255)
                                                        pic(client, 32)
                                                        create(client)

                                                    else

                                                        if hasItem(client, 722722, 1) then

                                                            text(client, "Kill a HillSpirit to get a GhostHorn. After you have three GhostHorns, I can make an EvilTooth for you.")
                                                            text(client, "")
                                                            link(client, "Make an EvilTooth.", 2)
                                                            link(client, "Make an ImmortalStone.", 3)
                                                            link(client, "Make an ImpureVigor", 4)
                                                            link(client, "I see.", 255)
                                                            pic(client, 32)
                                                            create(client)

                                                        else

                                                            if hasItem(client, 722723, 1) then

                                                                text(client, "After you get a Moss, DreamGrass and SoulAroma from HillSpirit`s minions, click EarthSeal to summon a ")
                                                                text(client, "HillSpirit. Kill that Spirit to get a GhostHorn.")
                                                                text(client, "")
                                                                link(client, "I see.", 255)
                                                                pic(client, 32)
                                                                create(client)

                                                            else

                                                                if hasItem(client, 722724, 1) then

                                                                    text(client, "After you get a Moss, DreamGrass and SoulAroma from HillSpirit`s minions, click EarthSeal to summon a ")
                                                                    text(client, "HillSpirit. Kill that Spirit to get a GhostHorn.")
                                                                    text(client, "")
                                                                    link(client, "I see.", 255)
                                                                    pic(client, 32)
                                                                    create(client)

                                                                else

                                                                    if hasItem(client, 722725, 1) then

                                                                        text(client, "After you get a Moss, DreamGrass and SoulAroma from HillSpirit`s minions, click EarthSeal to summon a ")
                                                                        text(client, "HillSpirit. Kill that Spirit to get a GhostHorn.")
                                                                        text(client, "")
                                                                        link(client, "I see.", 255)
                                                                        pic(client, 32)
                                                                        create(client)

                                                                    else

                                                                        text(client, "Halt! This place is filled with danger. If you move forward, you will risk your life.")
                                                                        link(client, "I come to accept the tests.", 5)
                                                                        link(client, "Too terrible.", 255)
                                                                        pic(client, 32)
                                                                        create(client)

                                                                    end

                                                                end

                                                            end

                                                        end

                                                    end

                                                end

                                            end

                                        end

                                    end

                                end

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            spendItem(client, 722731, 3)
            setUserStats(client, 61, 0, 1, true)
            setUserStats(client, 61, 1, 0, true)
            sendSysMsg(client, "Congratulations! You`ve completed the first stage of your tests.", 2005)

        end

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if hasItem(client, 722722, 3) then

                spendItem(client, 722722, 3)
                awardItem(client, "722721", 1)
                sendSysMsg(client, "You`ve got an Evil Tooth!", 2005)
                text(client, "After you have an EvilTooth, Moss, DreamGrass and SoulAroma, you may click Water Seal to summon a Swift ")
                text(client, "Devil, kill that devil to get a FeatherStone.")
                link(client, "I see.", 255)
                pic(client, 32)
                create(client)

            else

                text(client, "If you want me to make an Evil Tooth for you, please bring three Ghost Horns from the Hill Spirits.")
                link(client, "I will bring them soon.", 255)
                pic(client, 32)
                create(client)

            end

        end

    elseif (idx == 3) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if hasItem(client, 722726, 3) then

                spendItem(client, 722726, 3)
                awardItem(client, "722728", 1)
                sendSysMsg(client, "You`ve got an ImmortalStone!", 2005)
                text(client, "After you have an ImmortalStone, Moss, DreamGrass and SoulAroma, you may click FireSeal to summon a Banshee, ")
                text(client, "kill that Banshee to get a VigorFragment.")
                link(client, "I see.", 255)
                pic(client, 32)
                create(client)

            else

                text(client, "If you want me to make an ImmortalStone for you, please bring three FeatherStones from Swift Devils.")
                link(client, "I will bring them soon.", 255)
                pic(client, 32)
                create(client)

            end

        end

    elseif (idx == 4) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if hasItem(client, 722729, 3) then

                spendItem(client, 722729, 3)
                awardItem(client, "722730", 1)
                sendSysMsg(client, "You`ve got an ImpureVigor!", 2005)
                text(client, "After you have an ImpureVigor, you may click CleansingStove to summon a CleansingDevil, kill that devil to get a PureVigor.")
                link(client, "Thanks a lot.", 255)
                pic(client, 32)
                create(client)

            else

                text(client, "If you want me to make an ImpureVigor for you, please bring three VigorFragments from Banshees.")
                link(client, "I will bring them soon.", 255)
                pic(client, 32)
                create(client)

            end

        end

    elseif (idx == 5) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "So many people have come here for rebirth. All wanted to get reborn and start anew, but they didn`t know that they ")
            text(client, "had to go through many pains and dangers, they could lose their lives at any time. Since you can arrive here, you ")
            text(client, "must be powerful. Are you sure that you can pass the tests?")
            link(client, "Of course.", 6)
            link(client, "Let me think it over.", 255)
            pic(client, 32)
            create(client)

        end

    elseif (idx == 6) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "The monsters here are very ferocious. They are commanded by some devils. These devils are hiding in the seals. ")
            text(client, "They won`t appear unless the minions summon them. If you can make their boss appear and kill the boss, you will be ")
            text(client, "eligible to accept the tests.")
            link(client, "What can I do?", 7)
            link(client, "I changed my mind.", 255)
            pic(client, 32)
            create(client)

        end

    elseif (idx == 7) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Get a Moss, DreamGrass and SoulAroma from HillSpirit`s")
            text(client, "minions, then click EarthSeal to summon a Hill Spirit.")
            text(client, "After you get three GhostHorns from HillSpirits, I can make")
            text(client, "an EvilTooth for you. Take an EvilTooth, Moss, DreamGrass")
            text(client, "and SoulAroma to WaterSeal. Click that seal to Summon a")
            text(client, "SwiftDevil.")
            link(client, "Is SwiftDevil their boss?", 8)
            pic(client, 32)
            create(client)

        end

    elseif (idx == 8) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Their boss is Banshee hiding in FireSeal. Kill a SwiftDevil")
            text(client, "to get a FeatherStone. I can make an ImmortalStone from")
            text(client, "three FeatherStones. Take an ImmortalStone, Moss,")
            text(client, "DreamGrass and SoulAroma to FireSeal, click that seal to")
            text(client, "summon a Banshee. Kill that Banshee to get a VigorFragment.")
            link(client, "I`ve got to kill them.", 9)
            link(client, "Too dangerous.", 255)
            pic(client, 32)
            create(client)

        end

    elseif (idx == 9) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Wait! That Banshee was born of the essence of the evil")
            text(client, "spirits. Your killing can only destroy part of her vigor.")
            text(client, "After a short rest, she can come out to do all kinds of")
            text(client, "evil. You need to bring 3 VigorFragments to prove your ability.")
            link(client, "I see.", 255)
            pic(client, 32)
            create(client)

        end

    end

end
