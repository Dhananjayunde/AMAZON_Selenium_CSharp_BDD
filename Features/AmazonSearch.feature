Feature: Amazon Product Search
  As a user
  I want to search for products on Amazon
  So that I can view the search results for the vivo X300

  @smoke
  Scenario: Search for vivo X300 on Amazon
    Given I open the Amazon home page
    When I search for "vivo X300"
    Then I should see search results related to "vivo X300"
