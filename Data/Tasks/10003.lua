--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 6/21/2015 5:09:51 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10003(client, idx)
    name = "GuildDirector"
    face = 1

    if (idx == 0) then

        text(client, "I am in charge of all the guilds in Twin City. You may consult me for anything related to the guilds.")
        link(client, "Create a guild", 1)
        link(client, "Disband my guild", 2)
        link(client, "Donate money", 3)
        link(client, "Pass my leadership", 4)
        link(client, "Assign Deputy Guild Leader", 5)
        link(client, "Remove sb. from office", 6)
        link(client, "Inquire about a guild", 7)
        link(client, "Others", 8)
        pic(client, 7)
        create(client)

    elseif (idx == 1) then

        if getLevel(client) < 90 then

            text(client, "Sorry, you cannot create a guild before you reach level 90. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 7)
            create(client)

        else

            if getMoney(client) < 1000000 then

                text(client, "To create a guild, 1,000,000 silver will be charged. You do not have the required money.")
                link(client, "I see.", 255)
                pic(client, 7)
                create(client)

            else

                text(client, "What would you like to name your guild? A good name is better than great riches.")
                edit(client, "I name my guild...", 9, 15)
                link(client, "Let me think it over.", 255)
                pic(client, 7)
                create(client)

            end

        end

    elseif (idx == 2) then

        text(client, "Your guild cannot be retrieved once you disband it. You had better think it over.")
        link(client, "I decide to disband.", 10)
        link(client, "I changed my mind.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 3) then

        text(client, "If a guild has enough fund, it may be expanded rapidly. How much would you like to donate?")
        edit(client, "I want to donate??", 11, 8)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 4) then

        text(client, "Guild Leader bears heavy responsibilities. You had better pass your leadership to a competent candidate. Please think thrice be")
        edit(client, "I`ll pass my leadership to...", 12, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 5) then

        text(client, "A talent Deputy Leader can help you a lot. Who do you want to appoint?")
        edit(client, "I appoint??", 13, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 6) then

        text(client, "A good guild leader always rewards and punishes only those who really deserve. You had better appoint the best candidates.")
        edit(client, "I remove...from office", 14, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 7) then

        text(client, "Which guild would you like to inquire about?")
        edit(client, "I want to inquire about??", 15, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 8) then

        text(client, "I am in charge of all the guilds in Twin City. You may consult me for anything related to the guilds.")
        link(client, "Hostile to a guild", 16)
        link(client, "Reconcile with a guild", 17)
        link(client, "Ally with a guild", 18)
        link(client, "Un-ally with a guild", 19)
        link(client, "Guild List", 20)
        link(client, "Online Members", 21)
        link(client, "Expel Members", 22)
        link(client, "others", 23)
        pic(client, 7)
        create(client)

    elseif (idx == 9) then

        if createClan(client, 90, 1000000, 500000) then

            text(client, "Congrats! You have created your guild successfully. I believe your guild will be expanded rapidly and enjoy a good reputation.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 10) then

        destroyClan(client)

    elseif (idx == 11) then

        if donateToClan(client) then

            text(client, "Thanks a lot for your donation. Your guild must be expanded rapidly.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "You do not have the required money.")
            link(client, "I see.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 12) then

        if demiseFromClan(client) then

            text(client, "Congrats! You have passed your leadership successfully. I hope your guild could become more powerful.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "You do not have this member.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 13) then

        if setClanAssistant(client) then

            text(client, "Congrats! You have assigned Deputy Leader successfully. I wish your guild could become more powerful.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "You do not have this member.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 14) then

        if removeClanAssistant(client) then

            text(client, "Congrats! You have removed the target from office successfully. I wish your guild could become more powerful.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "You do not have this member.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 15) then


    elseif (idx == 16) then

        text(client, "Kindness and enmity coexist in Kungfu circle. All guild members should be of one mind. Which guild do you want to hostile to?")
        edit(client, "We want to hostile to...", 24, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 17) then

        text(client, "Better a lean peace than a fat victory. Which guild do you want to reconcile with?")
        edit(client, "We want to reconcile with??", 25, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 18) then

        text(client, "Many hands make light work. Alliance will be a great help to your guild, but Guild Leaders must team up to ally.")
        link(client, "We are ready for alliance.", 26)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 19) then

        text(client, "If the allies cannot be of one mind, It makes no sense to ally. Which guild do you want to un-ally?")
        edit(client, "I want to un-ally with??", 27, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 20) then


    elseif (idx == 21) then


    elseif (idx == 22) then

        text(client, "Which member would you like to expel?")
        edit(client, "Member Name", 30, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 23) then

        text(client, "I am in charge of all the guilds in Twin City. You may consult me for anything related to the guilds.")
        link(client, "Create Branch", 31)
        link(client, "Assign Branch Manager", 32)
        link(client, "Transfer fund.", 33)
        link(client, "Others", 34)
        pic(client, 7)
        create(client)

    elseif (idx == 24) then

        if addClanEnemy(client) then

            text(client, "Congrats! Your guild is hostile to the target. I wish your guild could be more powerful.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "This guild does not exist.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 25) then

        if removeClanEnemy(client) then

            text(client, "Congrats! You are reconciled with the target. I wish your guild could become more powerful.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "This guild does not exist.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 26) then

        if addClanAlly(client) then

            text(client, "Congrats! Your guild has allied with the target. I wish your guild could become more powerful.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 27) then

        if removeClanAlly(client) then

            text(client, "Congrats! You have unallied with the target successfully. I wish your guild could become more powerful.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "This guild does not exist.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 28) then


    elseif (idx == 29) then

        link(client, "I see.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 30) then



    elseif (idx == 31) then

        text(client, "What would you like to name your branch?")
        edit(client, "I name my Branch...", 35, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 32) then

        text(client, "What would you like to name your branch?")
        edit(client, "I name my Branch...", 36, 15)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 33) then

        if getClanRank(client) == 60 then

            text(client, "You can transfer your branch`s fund to your guild, so that your guild will get more powerful.")
            edit(client, "Guild Name", 37, 15)
            link(client, "Let me think it over.", 255)
            pic(client, 7)
            create(client)

        else

            if getClanRank(client) == 80 then

                text(client, "You can transfer your branch`s fund to your guild, so that your guild will get more powerful.")
                edit(client, "Guild Name", 37, 15)
                link(client, "Let me think it over.", 255)
                pic(client, 7)
                create(client)

            else

                text(client, "Sorry, only Branch Manager can tranfer fund to the guild.")
                link(client, "I see.", 255)
                pic(client, 7)
                create(client)

            end

        end

    elseif (idx == 34) then

        text(client, "I am in charge of all the guilds in Twin City. You may consult me for anything related to the guilds.")
        link(client, "Create a guild", 1)
        link(client, "Disband my guild", 2)
        link(client, "Donate money", 3)
        link(client, "Pass my leadership", 4)
        link(client, "Assign Deputy Guild Leader", 5)
        link(client, "Remove sb. from office", 6)
        link(client, "Inquire about a guild", 7)
        link(client, "Others", 8)
        pic(client, 7)
        create(client)

    elseif (idx == 35) then

        if createSubClan(client, 70, 500000, 500000) then

            text(client, "Congrats! You have created your branch successfully.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "The Guild Leader and Branch Manager should party up first, 500,000 guild fund and Branch Manager level 70 or above required.")
            link(client, "I see.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 36) then

        if changeSubClanLeader(client, 70) then

            text(client, "Congrats! You have assigned the Branch Manager successfully.")
            link(client, "Thanks.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "To create a branch, the guild leader should team up with the branch manager who is level 70 or above.")
            link(client, "I see.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 37) then


    elseif (idx == 38) then


    end

end
