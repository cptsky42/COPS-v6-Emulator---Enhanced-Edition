--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:10 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30(client, idx)
    name = "Taoist"
    face = 1

    if (idx == 0) then

        text(client, "After you learn spells, you may cast spells to kill enemies from a distance.")
        link(client, "Will you teach me?", 1)
        link(client, "Learn advanced spells.", 2)
        link(client, "Goodbye.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 1) then

        if hasMagic(client, 1001, -1) then

            text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
            link(client, "Thunder.", 3)
            link(client, "Cure.", 4)
            link(client, "Learn meditation.", 5)
            link(client, "Learn fire.", 6)
            pic(client, 6)
            create(client)

        else

            if getProfession(client) == 100 then

                text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                link(client, "Thunder.", 3)
                link(client, "Cure.", 4)
                link(client, "Learn meditation.", 5)
                link(client, "Learn fire.", 6)
                pic(client, 6)
                create(client)

            else

                if getProfession(client) == 101 then

                    text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                    link(client, "Thunder.", 3)
                    link(client, "Cure.", 4)
                    link(client, "Learn meditation.", 5)
                    link(client, "Learn fire.", 6)
                    pic(client, 6)
                    create(client)

                else

                    if getProfession(client) == 132 then

                        text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                        link(client, "Thunder.", 3)
                        link(client, "Cure.", 4)
                        link(client, "Learn meditation.", 5)
                        link(client, "Learn fire.", 6)
                        pic(client, 6)
                        create(client)

                    else

                        if getProfession(client) == 133 then

                            text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                            link(client, "Thunder.", 3)
                            link(client, "Cure.", 4)
                            link(client, "Learn meditation.", 5)
                            link(client, "Learn fire.", 6)
                            pic(client, 6)
                            create(client)

                        else

                            if getProfession(client) == 134 then

                                text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                                link(client, "Thunder.", 3)
                                link(client, "Cure.", 4)
                                link(client, "Learn meditation.", 5)
                                link(client, "Learn fire.", 6)
                                pic(client, 6)
                                create(client)

                            else

                                if getProfession(client) == 135 then

                                    text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                                    link(client, "Thunder.", 3)
                                    link(client, "Cure.", 4)
                                    link(client, "Learn meditation.", 5)
                                    link(client, "Learn fire.", 6)
                                    pic(client, 6)
                                    create(client)

                                else

                                    if getProfession(client) == 142 then

                                        text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                                        link(client, "Thunder.", 3)
                                        link(client, "Cure.", 4)
                                        link(client, "Learn meditation.", 5)
                                        link(client, "Learn fire.", 6)
                                        pic(client, 6)
                                        create(client)

                                    else

                                        if getProfession(client) == 143 then

                                            text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                                            link(client, "Thunder.", 3)
                                            link(client, "Cure.", 4)
                                            link(client, "Learn meditation.", 5)
                                            link(client, "Learn fire.", 6)
                                            pic(client, 6)
                                            create(client)

                                        else

                                            if getProfession(client) == 144 then

                                                text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                                                link(client, "Thunder.", 3)
                                                link(client, "Cure.", 4)
                                                link(client, "Learn meditation.", 5)
                                                link(client, "Learn fire.", 6)
                                                pic(client, 6)
                                                create(client)

                                            else

                                                if getProfession(client) == 145 then

                                                    text(client, "You are qualified to learn Thunder and cure. Which do you want to learn?")
                                                    link(client, "Thunder.", 3)
                                                    link(client, "Cure.", 4)
                                                    link(client, "Learn meditation.", 5)
                                                    link(client, "Learn fire.", 6)
                                                    pic(client, 6)
                                                    create(client)

                                                else

                                                    if getProfession(client) == 143 then

                                                        if hasMagic(client, 1000, 4) then

                                                            text(client, "You can learn Fire now. Would you like me to teach you?")
                                                            link(client, "Yes, please.", 7)
                                                            pic(client, 6)
                                                            create(client)

                                                        else

                                                            text(client, "Sorry, only Fire Taoists can learn this spell after they practice Thunder to level 4.")
                                                            link(client, "I see.", 255)
                                                            pic(client, 6)
                                                            create(client)

                                                        end

                                                    else

                                                        text(client, "Sorry, only Taoists can learn these spells.")
                                                        link(client, "I see. Thanks.", 255)
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

    elseif (idx == 2) then

        if getLevel(client) < 80 then

            text(client, "Sorry, you have to be a Fire Wizard over level 80 to learn that!")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if getProfession(client) == 143 then

                if hasMagic(client, 1002, -1) then

                    text(client, "You have learned Tornado.")
                    link(client, "Thank you.", 255)
                    pic(client, 6)
                    create(client)

                else

                    if hasMagic(client, 1001, 3) then

                        text(client, "Wow, you have grown in wisdom. Would like to learn a new spell?")
                        link(client, "Thanks.", 8)
                        pic(client, 6)
                        create(client)

                    else

                        text(client, "If you want to learn Tornado, you should practice Fire to level 3.")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 144 then

                    if hasMagic(client, 1002, -1) then

                        text(client, "You have learned Tornado.")
                        link(client, "Thank you.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        if hasMagic(client, 1001, 3) then

                            text(client, "Wow, you have grown in wisdom. Would like to learn a new spell?")
                            link(client, "Thanks.", 8)
                            pic(client, 6)
                            create(client)

                        else

                            text(client, "If you want to learn Tornado, you should practice Fire to level 3.")
                            link(client, "I see.", 255)
                            pic(client, 6)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 145 then

                        if hasMagic(client, 1002, -1) then

                            text(client, "You have learned Tornado.")
                            link(client, "Thank you.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            if hasMagic(client, 1001, 3) then

                                text(client, "Wow, you have grown in wisdom. Would like to learn a new spell?")
                                link(client, "Thanks.", 8)
                                pic(client, 6)
                                create(client)

                            else

                                text(client, "If you want to learn Tornado, you should practice Fire to level 3.")
                                link(client, "I see.", 255)
                                pic(client, 6)
                                create(client)

                            end

                        end

                    else

                        text(client, "Sorry, you have to be a Fire Wizard over level 80 to learn that!")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 3) then

        if hasMagic(client, 1000, -1) then

            text(client, "You have learned Thunder. Work hard so you can learn more spells.")
            link(client, "Thanks.", 255)
            pic(client, 6)
            create(client)

        else

            awardMagic(client, 1000, 0)
            text(client, "You have learned the elementary spells. You must train them to become powerful!")
            link(client, "Thanks, sir.", 255)
            create(client)

        end

    elseif (idx == 4) then

        if hasMagic(client, 1005, -1) then

            text(client, "You have learned Cure. Help others and you will grow strong.")
            link(client, "Thanks, sir.", 255)
            pic(client, 6)
            create(client)

        else

            awardMagic(client, 1005, 0)
            text(client, "You have learned Cure. Help others and you will grow strong.")
            link(client, "Thanks, sir.", 255)
            create(client)

        end

    elseif (idx == 5) then

        if getLevel(client) < 44 then

            text(client, "Sorry, only Water and Fire Taoists can learn this spell after they are level 44.")
            link(client, "I See.", 255)
            create(client)

        else

            if hasMagic(client, 1195, -1) then

                text(client, "Congrats! You have learned this spell.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            else

                if getProfession(client) == 142 then

                    awardMagic(client, 1195, 0)
                    text(client, "Congrats! You have learned meditation.")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)

                else

                    if getProfession(client) == 143 then

                        awardMagic(client, 1195, 0)
                        text(client, "Congrats! You have learned meditation.")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        if getProfession(client) == 144 then

                            awardMagic(client, 1195, 0)
                            text(client, "Congrats! You have learned meditation.")
                            link(client, "Thanks.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            if getProfession(client) == 145 then

                                awardMagic(client, 1195, 0)
                                text(client, "Congrats! You have learned meditation.")
                                link(client, "Thanks.", 255)
                                pic(client, 6)
                                create(client)

                            else

                                if getProfession(client) == 132 then

                                    awardMagic(client, 1195, 0)
                                    text(client, "Congrats! You have learned meditation.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    if getProfession(client) == 133 then

                                        awardMagic(client, 1195, 0)
                                        text(client, "Congrats! You have learned meditation.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 6)
                                        create(client)

                                    else

                                        if getProfession(client) == 134 then

                                            awardMagic(client, 1195, 0)
                                            text(client, "Congrats! You have learned meditation.")
                                            link(client, "Thanks.", 255)
                                            pic(client, 6)
                                            create(client)

                                        else

                                            if getProfession(client) == 135 then

                                                awardMagic(client, 1195, 0)
                                                text(client, "Congrats! You have learned meditation.")
                                                link(client, "Thanks.", 255)
                                                pic(client, 6)
                                                create(client)

                                            else

                                                text(client, "Sorry, only Fire and Water Taoist can learn this spell.")
                                                link(client, "I see.", 255)
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

    elseif (idx == 6) then

        if getProfession(client) == 142 then

            if hasMagic(client, 1000, 4) then

                text(client, "You can learn Fire now. Would you like me to teach you?")
                link(client, "Yes, please.", 7)
                pic(client, 6)
                create(client)

            else

                text(client, "Sorry, only Fire Taoists can learn this spell after they practice Thunder to level 4.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            end

        else

            if getProfession(client) == 143 then

                if hasMagic(client, 1000, 4) then

                    text(client, "You can learn Fire now. Would you like me to teach you?")
                    link(client, "Yes, please.", 7)
                    pic(client, 6)
                    create(client)

                else

                    text(client, "Sorry, only Fire Taoists can learn this spell after they practice Thunder to level 4.")
                    link(client, "I see.", 255)
                    pic(client, 6)
                    create(client)

                end

            else

                if getProfession(client) == 144 then

                    if hasMagic(client, 1000, 4) then

                        text(client, "You can learn Fire now. Would you like me to teach you?")
                        link(client, "Yes, please.", 7)
                        pic(client, 6)
                        create(client)

                    else

                        text(client, "Sorry, only Fire Taoists can learn this spell after they practice Thunder to level 4.")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    end

                else

                    if getProfession(client) == 145 then

                        if hasMagic(client, 1000, 4) then

                            text(client, "You can learn Fire now. Would you like me to teach you?")
                            link(client, "Yes, please.", 7)
                            pic(client, 6)
                            create(client)

                        else

                            text(client, "Sorry, only Fire Taoists can learn this spell after they practice Thunder to level 4.")
                            link(client, "I see.", 255)
                            pic(client, 6)
                            create(client)

                        end

                    else

                        text(client, "Sorry, only Fire Taoists can learn this spell after they practice Thunder to level 4.")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 7) then

        awardMagic(client, 1001, 0)
        text(client, "You have learned Fire. Power comes from training.")
        link(client, "Thanks, sir.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 8) then

        awardMagic(client, 1002, 0)
        text(client, "You have learned Tornado. It is very powerful. Please make good use of it.")
        link(client, "Thanks, sir.", 255)
        pic(client, 6)
        create(client)

    end

end
