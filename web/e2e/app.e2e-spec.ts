import { ConfidenceEstimationPathPage } from './app.po';

describe('confidence-estimation-path App', function() {
  let page: ConfidenceEstimationPathPage;

  beforeEach(() => {
    page = new ConfidenceEstimationPathPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
