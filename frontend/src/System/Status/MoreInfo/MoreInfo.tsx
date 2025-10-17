import React from 'react';
import DescriptionList from 'Components/DescriptionList/DescriptionList';
import DescriptionListItemDescription from 'Components/DescriptionList/DescriptionListItemDescription';
import DescriptionListItemTitle from 'Components/DescriptionList/DescriptionListItemTitle';
import FieldSet from 'Components/FieldSet';
import Link from 'Components/Link/Link';
import translate from 'Utilities/String/translate';

function MoreInfo() {
  return (
    <FieldSet legend={translate('MoreInfo')}>
      <DescriptionList>
        <DescriptionListItemTitle>
          {translate('HomePage')}
        </DescriptionListItemTitle>
        <DescriptionListItemDescription>
          <Link to="https://Readarr.tv/">Readarr.tv</Link>
        </DescriptionListItemDescription>

        <DescriptionListItemTitle>{translate('Wiki')}</DescriptionListItemTitle>
        <DescriptionListItemDescription>
          <Link to="https://wiki.servarr.com/Readarr">
            wiki.servarr.com/Readarr
          </Link>
        </DescriptionListItemDescription>

        <DescriptionListItemTitle>
          {translate('Forums')}
        </DescriptionListItemTitle>
        <DescriptionListItemDescription>
          <Link to="https://forums.Readarr.tv/">forums.Readarr.tv</Link>
        </DescriptionListItemDescription>

        <DescriptionListItemTitle>
          {translate('Twitter')}
        </DescriptionListItemTitle>
        <DescriptionListItemDescription>
          <Link to="https://twitter.com/Readarrtv">@Readarrtv</Link>
        </DescriptionListItemDescription>

        <DescriptionListItemTitle>
          {translate('Discord')}
        </DescriptionListItemTitle>
        <DescriptionListItemDescription>
          <Link to="https://discord.Readarr.tv/">discord.Readarr.tv</Link>
        </DescriptionListItemDescription>

        <DescriptionListItemTitle>{translate('IRC')}</DescriptionListItemTitle>
        <DescriptionListItemDescription>
          <Link to="irc://irc.libera.chat/#Readarr">
            {translate('IRCLinkText')}
          </Link>
        </DescriptionListItemDescription>
        <DescriptionListItemDescription>
          <Link to="https://web.libera.chat/?channels=#Readarr">
            {translate('LiberaWebchat')}
          </Link>
        </DescriptionListItemDescription>

        <DescriptionListItemTitle>
          {translate('Donations')}
        </DescriptionListItemTitle>
        <DescriptionListItemDescription>
          <Link to="https://Readarr.tv/donate">Readarr.tv/donate</Link>
        </DescriptionListItemDescription>

        <DescriptionListItemTitle>
          {translate('Source')}
        </DescriptionListItemTitle>
        <DescriptionListItemDescription>
          <Link to="https://github.com/Readarr/Readarr/">
            github.com/Readarr/Readarr
          </Link>
        </DescriptionListItemDescription>

        <DescriptionListItemTitle>
          {translate('FeatureRequests')}
        </DescriptionListItemTitle>
        <DescriptionListItemDescription>
          <Link to="https://forums.Readarr.tv/">forums.Readarr.tv</Link>
        </DescriptionListItemDescription>
        <DescriptionListItemDescription>
          <Link to="https://github.com/Readarr/Readarr/issues">
            github.com/Readarr/Readarr/issues
          </Link>
        </DescriptionListItemDescription>
      </DescriptionList>
    </FieldSet>
  );
}

export default MoreInfo;
