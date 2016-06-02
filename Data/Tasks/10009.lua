--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:40 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10009(client, idx)
    name = "Mr.Nosy"
    face = 1

    if (idx == 0) then

        if getProfession(client) == 100 then

            text(client, "Hey! You have chosen Taoist class. Taoist are skilled at spells. You can cast spells to kill enemies from a distance.")
            link(client, "Please tell me more.", 1)
            link(client, "Consult others.", 255)
            pic(client, 6)
            create(client)

        else

            if getProfession(client) == 20 then

                text(client, "You chose to be a brave warrior. A great choice! Warrior can wear all kinds of weapons and armors, and is good at close combat.")
                link(client, "Please tell me more.", 2)
                link(client, "Consult others.", 255)
                pic(client, 6)
                create(client)

            else

                if getProfession(client) == 10 then

                    text(client, "Welcome! A great choice. Trojans have more health points than any other class, and Trojans can equip two handed weapons.")
                    link(client, "Please tell me more.", 3)
                    link(client, "Consult others.", 255)
                    pic(client, 6)
                    create(client)

                else

                    if getProfession(client) == 40 then

                        text(client, "Archers use bow and arrow to shoot. They can fly and learn special skills, fire flames at any who dare cross their path.")
                        link(client, "Please tell me more.", 4)
                        link(client, "Consult others.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        if getProfession(client) == 30 then

                            text(client, "You have chosen to be a knight. A great choice.")
                            link(client, "Please tell more.", 5)
                            link(client, "Consult others.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            text(client, "Welcome back! Is everything going on well with you? My advice must have been of great help to you.")
                            link(client, "Your advice is helpful.", 255)
                            pic(client, 6)
                            create(client)

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        text(client, "You can learn basic spells from Taoist Star and learn more in Job Center of Twin City later. Do not let enemies")
        text(client, "approach you.To cast a spell, just click on Skill button to select a spell, then right click on the enemies within the")
        text(client, "attack range. Take mana potions and HP potions when you are hunting. Go to get promoted in Job Center after level 15.")
        text(client, "If a monster`s in Green name, it`s weaker than you; in white, it`s about the same; in red, it`s stronger; in black, it`s deadly")
        link(client, "Thanks.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 2) then

        text(client, "Warrior is skilled at melee. I suggest you equip the best armors and weapons, then click on the enemies to hit.")
        text(client, "After you reach level 15, you can go to the Job Center in Twin City to get promoted and learn XP skills.")
        text(client, "If a monster`s in Green name, it`s weaker than you; in white, it`s about the same; in red, it`s stronger; in black, it`s deadly")
        link(client, "Thanks.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 3) then

        text(client, "Remember to equip good armors and weapons, carry enough potions, and then go to the wild to kill monsters.")
        text(client, "After you reach level 15, you can go to the Job Center in Twin City to get promoted and learn XP skills.")
        text(client, "If a monster`s in Green name, it`s weaker than you; in white, it`s about the same; in red, it`s stronger; in black, it`s deadly")
        link(client, "Thanks.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 4) then

        text(client, "Archers use arrows to shoot, so do not forget to carry enough arrows and Healing potions with you. As you")
        text(client, "are weak at melee, you had better get away from a group of monsters and attack from distance.")
        text(client, "If a monster`s in Green name, it`s weaker than you; in white, it`s about the same; in red, it`s stronger; in black, it`s deadly")
        link(client, "Thanks.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 5) then

        text(client, "As knights use mana to attack enemies, mana is very important to knights.")
        text(client, "attack range. Take mana potions and HP potions when you are hunting. Go to get promoted in Job Center after level 15.")
        text(client, "If a monster`s in Green name, it`s weaker than you; in white, it`s about the same; in red, it`s stronger; in black, it`s deadly")
        link(client, "Thanks.", 255)
        pic(client, 6)
        create(client)

    end

end
