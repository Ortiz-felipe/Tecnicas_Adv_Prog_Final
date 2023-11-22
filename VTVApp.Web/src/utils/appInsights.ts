// appInsights.ts
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { ReactPlugin } from '@microsoft/applicationinsights-react-js';

const reactPlugin = new ReactPlugin();

const appInsights = new ApplicationInsights({
  config: {
    instrumentationKey: '1467b82f-3280-4175-83a3-bf4e98b99c46',
    extensions: [reactPlugin],
    enableAutoRouteTracking: true
    /* ...Other Configuration Options... */
  }
});

appInsights.loadAppInsights();
appInsights.trackPageView();

export default appInsights;
